#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

use base64::{engine::general_purpose::STANDARD, Engine as _};
use serde::Serialize;
use std::fs;
use std::path::PathBuf;

#[derive(Serialize)]
#[serde(rename_all = "camelCase")]
struct OpenImage {
    file_name: String,
    mime: String,
    image_data: String,
    json_path: String,
    json_text: Option<String>,
}

fn load_image(path: PathBuf) -> Result<OpenImage, String> {
    let bytes = fs::read(&path).map_err(|error| format!("读取图片失败：{error}"))?;
    let file_name = path
        .file_name()
        .and_then(|name| name.to_str())
        .ok_or_else(|| "图片文件名不是有效的 UTF-8".to_string())?
        .to_string();
    let extension = path
        .extension()
        .and_then(|value| value.to_str())
        .unwrap_or_default()
        .to_ascii_lowercase();
    let mime = match extension.as_str() {
        "png" => "image/png",
        "jpg" | "jpeg" => "image/jpeg",
        "bmp" => "image/bmp",
        "gif" => "image/gif",
        "webp" => "image/webp",
        _ => return Err("仅支持 PNG、JPG、BMP、GIF 或 WebP 图片".to_string()),
    };
    let json_path = path.with_extension("json");
    let json_text = match fs::read_to_string(&json_path) {
        Ok(text) => Some(text),
        Err(error) if error.kind() == std::io::ErrorKind::NotFound => None,
        Err(error) => return Err(format!("读取同名 JSON 失败：{error}")),
    };

    Ok(OpenImage {
        file_name,
        mime: mime.to_string(),
        image_data: STANDARD.encode(bytes),
        json_path: json_path.to_string_lossy().into_owned(),
        json_text,
    })
}

#[tauri::command]
fn open_image() -> Result<Option<OpenImage>, String> {
    let Some(path) = rfd::FileDialog::new()
        .add_filter("图像", &["png", "jpg", "jpeg", "bmp", "gif", "webp"])
        .pick_file()
    else {
        return Ok(None);
    };
    load_image(path).map(Some)
}

#[tauri::command]
async fn open_image_path(path: String) -> Result<OpenImage, String> {
    let path = PathBuf::from(path);
    if !path.is_absolute() {
        return Err("图片路径必须是绝对路径".to_string());
    }
    tauri::async_runtime::spawn_blocking(move || load_image(path))
        .await
        .map_err(|error| format!("读取图片任务失败：{error}"))?
}

fn checked_json_path(path: &str) -> Result<PathBuf, String> {
    let path = PathBuf::from(path);
    if !path.is_absolute() {
        return Err("JSON 路径必须是绝对路径".to_string());
    }
    let is_json = path
        .extension()
        .and_then(|value| value.to_str())
        .is_some_and(|value| value.eq_ignore_ascii_case("json"));
    if !is_json {
        return Err("仅允许读写 .json 文件".to_string());
    }
    Ok(path)
}

#[tauri::command]
async fn read_json(path: String) -> Result<String, String> {
    tauri::async_runtime::spawn_blocking(move || {
        let path = checked_json_path(&path)?;
        fs::read_to_string(&path).map_err(|error| format!("读取 JSON 失败：{error}"))
    })
    .await
    .map_err(|error| format!("读取 JSON 任务失败：{error}"))?
}

#[tauri::command]
async fn write_json(path: String, contents: String) -> Result<(), String> {
    tauri::async_runtime::spawn_blocking(move || {
        let path = checked_json_path(&path)?;
        let temp_path = path.with_extension(format!("json.{}.tmp", std::process::id()));
        fs::write(&temp_path, contents).map_err(|error| format!("写入临时 JSON 失败：{error}"))?;
        fs::rename(&temp_path, &path).map_err(|error| format!("保存 JSON 失败：{error}"))?;
        Ok(())
    })
    .await
    .map_err(|error| format!("写入 JSON 任务失败：{error}"))?
}

fn main() {
    if let Err(error) = tauri::Builder::default()
        .invoke_handler(tauri::generate_handler![
            open_image,
            open_image_path,
            read_json,
            write_json
        ])
        .run(tauri::generate_context!())
    {
        eprintln!("EditRegionTauri failed to start: {error}");
    }
}
