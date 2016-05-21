using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlConfigMinifier.UIs.XmlConfigMinifier
{
    public static class XmlConfigMinifierCore
    {
        public static string Minify(
            string xmlString,
            int indention,
            bool removeComments,
            bool preserveWhiteSpace)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.PreserveWhitespace = preserveWhiteSpace;
            xDoc.LoadXml(xmlString);
            if (removeComments)
            {
                XmlNodeList list = xDoc.SelectNodes("//comment()");
                foreach (XmlNode node in list)
                {
                    node.ParentNode.RemoveChild(node);
                }
            }

            string xml;
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter xtw = new XmlTextWriter(sw))
                {
                    if (indention > 0)
                    {
                        xtw.IndentChar = ' ';
                        xtw.Indentation = indention;
                        xtw.Formatting = System.Xml.Formatting.Indented;
                    }

                    xDoc.WriteContentTo(xtw);
                    xtw.Close();
                    sw.Close();
                }
                xml = sw.ToString();
            }

            return xml;
        }
    }
}
