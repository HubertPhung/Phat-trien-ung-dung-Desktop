using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocFileXML_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Load XML file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("..\\..\\books.xml"); // Đặt file books.xml trong thư mục bin\Debug

            // Lấy danh sách các node "book"
            var nodeList = xmlDoc.DocumentElement.SelectNodes("/catalog/book");

            // Duyệt từng node
            foreach (XmlNode node in nodeList)
            {
                // Đọc attribute
                var isbn = node.Attributes["ISBN"].Value;

                // Đọc child nodes
                var title = node.SelectSingleNode("title").InnerText;
                var price = node.SelectSingleNode("price").InnerText;

                // Đọc descendant nodes
                var firstName = node.SelectSingleNode("author/first-name").InnerText;
                var lastName = node.SelectSingleNode("author/last-name").InnerText;

                // In kết quả
                Console.WriteLine("{0,-15}{1,-50}{2,-15}{3,-15}{4,6}",
                    isbn, title, firstName, lastName, price);
            }

            Console.ReadLine();
        }
    }
}
