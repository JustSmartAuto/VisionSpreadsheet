using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

namespace OpencvDemo
{
    static class csEnumReadWriter
    {
        public static Enum getEnumByValue(Type enumType, string value)
        {
            return Enum.Parse(enumType, value) as Enum;
        }

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

    }
    static class csXMLReaderWriter
    {
        private static string readFileName = "";
        private static string writeFileName = "";
        private static XmlDocument xmlDoc;
        private static XmlNode xmlRoot;
        private static XmlElement xmlNode;
        private static XmlNodeList listSubNode;

        public static bool OpenReader(string fileName, string rootName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return false;
                }
                readFileName = fileName;
                xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlRoot = xmlDoc.SelectSingleNode(rootName);//查找<Root>
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool ReadNode(string node)
        {
            int nNode = -1;
            try
            {

                for (int i = 0; i < xmlRoot.ChildNodes.Count; i++)
                {
                    if (xmlRoot.ChildNodes[i].Name == node)
                    {
                        listSubNode = xmlRoot.ChildNodes[i].ChildNodes;
                        nNode = i;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return nNode >= 0;
        }
        public static bool ReadSubNode(string subNode, out string subNodeValue)
        {
            subNodeValue = "";
            try
            {
                for (int i = 0; i < listSubNode.Count; i++)
                {
                    if (listSubNode[i].Name == subNode)
                    {
                        subNodeValue = listSubNode[i].InnerText;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool CloseReader()
        {
            return true;
        }
        public static bool OpenWriter(string fileName, string rootName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(rootName);
                writer.WriteEndElement();
                writer.Close();
                System.Threading.Thread.Sleep(100);

                xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlRoot = xmlDoc.SelectSingleNode(rootName);
                writeFileName = fileName;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool WriteNode(string node)
        {
            try
            {
                xmlNode = xmlDoc.CreateElement(node);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }
        public static bool WriteSubNode(string subNode, string subNodeValve)
        {
            try
            {
                XmlElement xeSub1 = xmlDoc.CreateElement(subNode);
                xeSub1.InnerText = subNodeValve;
                xmlNode.AppendChild(xeSub1);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool AddNode()
        {
            try
            {
                xmlRoot.AppendChild(xmlNode);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool CloseWrite()
        {
            try
            {
                xmlDoc.Save(writeFileName);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
    static class csBinaryReaderWriter
    {
        private static BinaryWriter bw;
        private static FileStream fsw;
        private static BinaryReader br;
        private static FileStream fsr;
        public static bool OpenReader(string fileName)
        {
            fsr = new FileStream(fileName, FileMode.OpenOrCreate);
            br = new BinaryReader(fsr);
            return true;
        }

        public static void Read<T>(ref T val)
        {
            try
            {
                byte[] arr = br.ReadBytes(Marshal.SizeOf(val));
                if (arr.Length == 0)
                {
                    return;
                }
                int structSize = Marshal.SizeOf(typeof(T));
                IntPtr ptemp = Marshal.AllocHGlobal(structSize);
                Marshal.Copy(arr, 0, ptemp, structSize);
                T rs = (T)Marshal.PtrToStructure(ptemp, typeof(T));
                Marshal.FreeHGlobal(ptemp);
                val = rs;
            }
            catch
            {

            }
        }

        public static bool CloseReader()
        {
            fsr.Close();
            br.Close();
            return true;
        }
        public static bool OpenWriter(string fileName)
        {
            fsw = new FileStream(fileName, FileMode.OpenOrCreate);
            bw = new BinaryWriter(fsw);
            return true;
        }
        public static void Write<T>(T val)
        {
            try
            {
                byte[] tempDest = new byte[Marshal.SizeOf(val)];
                int valSize = Marshal.SizeOf(typeof(T));
                byte[] tempSrc = new byte[valSize];
                // 分配结构体大小的内存空间 
                IntPtr structPtr = Marshal.AllocHGlobal(valSize);
                // 将结构体拷到分配好的内存空间 
                Marshal.StructureToPtr(val, structPtr, false);
                // 从内存空间拷到byte数组 
                Marshal.Copy(structPtr, tempSrc, 0, valSize);
                // 释放内存空间 
                Marshal.FreeHGlobal(structPtr);
                Array.Copy(tempSrc, tempDest, tempSrc.Length);
                bw.Write(tempDest);
            }
            catch
            {

            }

        }
        public static bool CloseWriter()
        {
            bw.Flush();
            fsw.Close();
            bw.Close();
            return true;
        }

    }
    static class csBinaryReaderWriterBase
    {
        private static BinaryWriter bw;
        private static FileStream fsw;
        private static BinaryReader br;
        private static FileStream fsr;
        public static bool OpenReader(string fileName)
        {
            fsr = new FileStream(fileName, FileMode.OpenOrCreate);
            br = new BinaryReader(fsr);
            return true;
        }

        public static void ReadInt(ref int val)
        {
            val = br.ReadInt32();
        }

        public static void ReadBool(ref bool val)
        {
            val = br.ReadBoolean();
        }

        public static void ReadDouble(ref double val)
        {
            val = br.ReadDouble();
        }

        public static void ReadBytes(ref byte[] val, int nCount)
        {
            val = br.ReadBytes(nCount);
        }

        public static void ReadChars(ref char[] val, int nCount)
        {
            val = br.ReadChars(nCount);
        }

        public static void ReadString(ref string val)
        {
            val = br.ReadString();
        }

        public static bool CloseReader()
        {
            fsr.Close();
            br.Close();
            return true;
        }
        public static bool OpenWriter(string fileName)
        {
            fsw = new FileStream(fileName, FileMode.OpenOrCreate);
            bw = new BinaryWriter(fsw);
            return true;
        }

        public static void WriteInt(int val)
        {
            bw.Write(val);
        }

        public static void WriteBool(bool val)
        {
            bw.Write(val);
        }

        public static void WriteDouble(double val)
        {
            bw.Write(val);
        }

        public static void WriteBytes(byte[] val)
        {
            bw.Write(val);
        }

        public static void WriteChars(char[] val)
        {
            bw.Write(val);
        }

        public static void WriteString(string val)
        {
            bw.Write(val);
        }

        public static bool CloseWriter()
        {
            bw.Flush();
            fsw.Close();
            bw.Close();
            return true;
        }

    }
    static class csTxtReaderWriter
    {
        private static StreamReader sr;
        private static FileInfo finfo;
        private static FileStream fs;
        private static StreamWriter sw;
        public static bool OpenReader(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            sr = new StreamReader(fileName);
            return true;
        }
        public static bool ReadTxtLine(out string value)
        {
            value = "";
            try
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    return false;
                }
                value = line;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static bool CloseReader()
        {
            if (sr != null)
            {
                sr.Close();
            }
            return true;
        }
        public static bool OpenWriter(string fileName)
        {
            try
            {
                finfo = new FileInfo(fileName);
                sw = finfo.CreateText();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool WriteTxtLine(string value)
        {
            try
            {
                sw.WriteLine(value);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public static bool CloseWriter()
        {
            try
            {
                sw.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
