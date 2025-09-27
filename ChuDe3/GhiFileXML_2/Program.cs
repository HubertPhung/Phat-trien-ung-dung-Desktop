using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GhiFileXML_2
{
    [XmlType("book")]
    public class Book
    {
        [XmlAttribute("ISBN")]
        public string ISBN { get; set; }

        [XmlAttribute("yearpublished")]
        public int YearPublished { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }

    internal class Program
    {
        private static void SaveToXmlFile(List<Book> books)
        {
            // Serialize List<Book> with root element <catalog>
            var serializer = new XmlSerializer(typeof(List<Book>), new XmlRootAttribute("catalog"));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true, // we will manually write PI/DOCTYPE before the root
                Encoding = Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create("books.xml", settings))
            {
                // Optional: XML declaration will be omitted; write stylesheet PI and DOCTYPE as in the original sample
                string pi = "type='text/xsl' href='book.xsl'";
                writer.WriteProcessingInstruction("xml-stylesheet", pi);

                writer.WriteDocType("catalog", null, null, "<!ENTITY h 'hardcover'>");

                writer.WriteComment("This is a book sample XML");

                // Serialize the list under <catalog>
                serializer.Serialize(writer, books, null);

                writer.Flush();
            }
        }

        public static void Main(string[] args)
        {
            // Prepare sample data
            var books = new List<Book>
            {
                new Book
                {
                    ISBN = "9831123212",
                    YearPublished = 2002,
                    Author = "Mahesh Chand",
                    Title = "Visual C# Programming",
                    Price = 44.95m
                },
                // Add more books as needed
                new Book
                {
                    ISBN = "9780131103627",
                    YearPublished = 1988,
                    Author = "Brian W. Kernighan; Dennis M. Ritchie",
                    Title = "The C Programming Language",
                    Price = 39.99m
                }
            };

            SaveToXmlFile(books);
            Console.WriteLine("books.xml da duoc tao thanh cong!");
        }
    }
}
