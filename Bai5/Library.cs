using System.Xml.Linq;
using System.Data;
using System.Linq;

namespace Bai5
{
    public class Library
    {
        public XDocument open(string url)
        {
            // Hàm load một file xml vào đối tượng XDocument[cite: 1]
            try
            {
                return XDocument.Load(url);
            }
            catch
            {
                return null;
            }
        }

        public void insert(string scode, string sname, string sclass, string saddress, string path)
        {
            XDocument doc = open(path);
            if (doc != null && doc.Root != null)
            {
                doc.Descendants("ute").Elements("student").Last().AddAfterSelf(new XElement("student",
                    new XAttribute("code", scode),
                    new XAttribute("name", sname),
                    new XAttribute("class", sclass),
                    new XAttribute("address", saddress)
                ));
                doc.Save(path);
            }
        }

        public void update(string scode, string sname, string sclass, string saddress, string path)
        {
            XDocument doc = open(path);
            if (doc != null)
            {
                if (doc.Descendants("student").Where(x => x.Attribute("code").Value.Equals(scode)).Count() == 1)
                {
                    // Nếu đã tồn tại sinh viên có ID này rồi thì thực hiện cập nhật[cite: 1]
                    XElement ele = doc.Descendants("student").Where(x => x.Attribute("code").Value.Equals(scode)).First();
                    ele.SetAttributeValue("name", sname);
                    ele.SetAttributeValue("class", sclass);
                    ele.SetAttributeValue("address", saddress);
                }
                doc.Save(path);
            }
        }

        public XElement find(string scode, string path)
        {
            XDocument doc = open(path);
            if (doc != null && doc.Root != null)
            {
                var list = doc.Root.Nodes();
                foreach (XElement el in list)
                {
                    if (el is XElement element && element.Attribute("code") != null)
                    {
                        if (scode == element.Attribute("code").Value)
                        {
                            return element;
                        }
                    }
                }
            }
            return null;
        } // end of method[cite: 1]

        public bool delete(string scode, string path)
        {
            XDocument doc = open(path);
            if (doc != null && doc.Root != null)
            {
                var list = doc.Root.Nodes();
                foreach (XElement el in list)
                {
                    if (el is XElement element && element.Attribute("code") != null)
                    {
                        if (scode == element.Attribute("code").Value)
                        {
                            element.Remove();
                            doc.Save(path);
                            return true;
                        }
                    }
                }
            }
            return false;
        } // end of method[cite: 1]
    }
}