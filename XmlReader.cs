using Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xml2CSharp;

namespace Xml_To_Excel
{
    public interface IReadXmlFoder
    {
        Task<IEnumerable<Test>> Read(string directory, Encoding enc);
    }
    public class ReadXmlFoder : IReadXmlFoder
    {
        public async Task<IEnumerable<Test>> Read(string directory, Encoding enc)
        => await Task.Run(() =>
        {
            return new DirectoryInfo(directory)
                .EnumerateFiles("*.xml", SearchOption.AllDirectories)
                .Select(fi =>
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Bill));
                    string tmp = string.Empty;
                    using (StreamReader sr = new StreamReader(fi.FullName, enc))
                    {
                        tmp += sr.ReadToEndAsync().Result;
                    }

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(tmp.Replace("\x0c", ""));
                    return DeserializeFromXmlDocument(xDoc).Result;
                });
        });
        static async Task<Test> DeserializeFromXmlDocument(XmlDocument doc)
        => await Task.Run(() =>
        {
            XmlSerializer seri = new XmlSerializer(typeof(Test));
            Test test;
            using (var reader = new XmlNodeReader(doc.DocumentElement))
            {
                test = (Test)seri.Deserialize(reader);
            }
            return test;
        });
    }
}