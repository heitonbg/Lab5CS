using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string[]> errorDictionary = new Dictionary<string, string[]>
        {
            {"привет", new[] {"првиет", "пирвет", "приевт", "првет"}},
            {"пока", new[] {"пкоа", "поак", "пако"}},
            {"программирование", new[] {"програмирование", "програмирвоание", "прогрммирование"}}
        };

        Console.WriteLine("Введите путь к директории с текстовыми файлами:");
        string directoryPath = Console.ReadLine();

        foreach (string filePath in Directory.GetFiles(directoryPath, "*.txt"))
        {
            string content = File.ReadAllText(filePath, Encoding.UTF8);

            foreach (var pair in errorDictionary)
            {
                foreach (string error in pair.Value)
                {
                    content = content.Replace(error, pair.Key);
                }
            }

            content = Regex.Replace(content,
                @"\((\d{3})\)\s(\d{3})-(\d{2})-(\d{2})",
                "+380 $1 $2 $3 $4");

            File.WriteAllText(filePath, content, Encoding.UTF8);

            Console.WriteLine($"Файл {Path.GetFileName(filePath)} обработан.");
        }

        Console.WriteLine("Обработка завершена.");
    }
}