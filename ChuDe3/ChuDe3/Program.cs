using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChuDe3
{
    internal class Program
    {
        private static void WriteReadText(string filename, string[] text)
        {
            // Ghi du lieu text vao file
            File.WriteAllLines(filename, text);

            Console.WriteLine("Da ghi du lieu vao file: " + filename);
            Console.WriteLine("Noi dung file la:");

            // Doc du lieu tu file va in ra man hinh
            foreach (string line in File.ReadAllLines(filename))
            {
                Console.WriteLine(line);
            }
        }
        static void Main(string[] args)
        {
            // Mang chuoi can ghi vao file
            string[] lines = {
            "Xin chao, day la dong 1",
            "Day la dong 2",
            "Hoc C# lam viec voi File IO"
        };

            // Ten file
            string filename = "test.txt";

            // Goi ham thuc hien
            WriteReadText(filename, lines);

            Console.ReadLine();
        }
    }
}
