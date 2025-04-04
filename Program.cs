using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
  private static readonly Dictionary<string, string[]> _errorDictionary = new Dictionary<string, string[]>
  {
    {"привет", new[] {"првиет", "пирвет", "приевт", "првет"}},
    {"пока", new[] {"пкоа", "поак", "пако"}},
    {"программирование", new[] {"програмирование", "програмирвоание", "прогрммирование"}}
  };

  static void Main(string[] args)
  {
    Console.WriteLine("Введите путь к директории с текстовыми файлами:");
    string directoryPath = Console.ReadLine();

    if (!Directory.Exists(directoryPath))
    {
      Console.WriteLine("Указанная директория не существует");
      return;
    }

    ProcessTextFiles(directoryPath);
    Console.WriteLine("Обработка завершена.");
  }

  private static void ProcessTextFiles(string directoryPath)
  {
    foreach (string filePath in Directory.GetFiles(directoryPath, "*.txt"))
    {
      string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
      fileContent = FixSpellingErrors(fileContent);
      fileContent = FormatPhoneNumbers(fileContent);
      
      File.WriteAllText(filePath, fileContent, Encoding.UTF8);
      
      Console.WriteLine($"Файл {Path.GetFileName(filePath)} обработан.");
    }
  }

  private static string FixSpellingErrors(string content)
  {
    foreach (KeyValuePair<string, string[]> wordPair in _errorDictionary)
    {
      foreach (string incorrectWord in wordPair.Value)
      {
        content = content.Replace(incorrectWord, wordPair.Key);
      }
    }
    
    return content;
  }

  private static string FormatPhoneNumbers(string content)
  {
    return Regex.Replace(
      content,
      @"\((\d{3})\)\s(\d{3})-(\d{2})-(\d{2})",
      "+380 $1 $2 $3 $4"
    );
  }
}