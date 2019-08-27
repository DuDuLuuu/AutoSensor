using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace ZW.Common.Properties
{
    public class EncryptProperties : System.Collections.Hashtable
    {
        private ArrayList keys = new ArrayList();

        private String fileName = string.Empty;//要读写的Properties文件名
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">文件名</param>
        public EncryptProperties(String fileName)
        {
            this.setFileName(fileName);
            load(fileName);
        }

        public EncryptProperties()
        {
        }

        protected void setFileName(string filePath)
        {
            this.fileName = filePath;
        }

        /// <summary>
        /// 重写Add方法,实现按添加顺序排列
        /// </summary>
        /// <param name="key">key</param>

        ///<param name="value">value</param>
        /// <returns></returns>
        public override void Add(object key, object value)
        {
            if (base.ContainsKey(key) == false)
            {
                base.Add(key, value);
                keys.Add(key);
            }
            else
            {
                base[key] = value;
            }
        }

        public override ICollection Keys
        {
            get
            {
                return keys;
            }
        }

        /// <summary>
        /// 从流加载配置数据
        /// </summary>
        /// <param name="stream"></param>
        public void LoadStream(Stream stream)
        {
            char[] convertBuf = new char[1024];

            int limit;
            int keyLen;
            int valueStart;
            char c;
            string bufLine = string.Empty;
            bool hasSep;
            bool precedingBackslash;

            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    bufLine = sr.ReadLine();
                    limit = bufLine.Length;
                    keyLen = 0;
                    valueStart = limit;
                    hasSep = false;

                    precedingBackslash = false;
                    if (bufLine.StartsWith("#"))
                        keyLen = bufLine.Length;

                    while (keyLen < limit)
                    {
                        c = bufLine[keyLen];
                        if ((c == '=' || c == ':') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            hasSep = true;
                            break;
                        }
                        else if ((c == ' ' || c == '\t' || c == '\f') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            break;
                        }
                        if (c == '\\')
                        {
                            precedingBackslash = !precedingBackslash;
                        }
                        else
                        {
                            precedingBackslash = false;
                        }
                        keyLen++;
                    }

                    while (valueStart < limit)
                    {
                        c = bufLine[valueStart];
                        if (c != ' ' && c != '\t' && c != '\f')
                        {
                            if (!hasSep && (c == '=' || c == ':'))
                            {
                                hasSep = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                        valueStart++;
                    }

                    string key = bufLine.Substring(0, keyLen);

                    string values = bufLine.Substring(valueStart, limit - valueStart);

                    if (key == "")
                        key += "#";
                    while (key.StartsWith("#") & this.Contains(key))
                    {
                        key += "#";
                    }

                    this.Add(key, values);
                }
            }
        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="filePath">要导入的文件</param>
        /// <returns></returns>
        public virtual void load(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                return;
            }
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                LoadStream(fileStream);
            }
        }

        public virtual void load()
        {
            load(fileName);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">要保存的Properties文件</param>
        /// <returns></returns>
        public virtual void save(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fileStream = File.Create(filePath))
            {
                saveStream(fileStream);
            }            
        }

        public void save()
        {
            save(fileName);
        }

        public void saveStream(Stream stream)
        {
            StreamWriter sw = new StreamWriter(stream);
            foreach (object item in keys)
            {
                String key = (String)item;
                String val = (String)this[key];
                if (key.StartsWith("#"))
                {
                    if (val == "")
                    {
                        sw.WriteLine(key);
                    }
                    else
                    {
                        sw.WriteLine(val);
                    }
                }
                else
                {
                    sw.WriteLine(key + "=" + val);
                }
            }
            sw.Flush();
        }
      

        public string getPropertiesText()
        {
            StringBuilder buf = new StringBuilder();
            foreach (object item in keys)
            {
                String key = (String)item;
                String val = (String)this[key];
                if (key.StartsWith("#"))
                {
                    if (val == "")
                    {
                        buf.AppendLine(key);
                    }
                    else
                    {
                        buf.AppendLine(val);
                    }
                }
                else
                {
                    buf.AppendLine(key + "=" + val);
                }
            }
            return buf.ToString();
        }
    }
}
