// See https://aka.ms/new-console-template for more information
using static System.Environment;
using static System.IO.Path;
using static System.Console;
using System.Xml;
using System.IO.Compression;

internal class Program
{
    public static string dir = Combine(
        GetFolderPath(SpecialFolder.Personal),
        "Code", "Chapter09", "OutputFiles");

    private static void Main(string[] args)
    {
        WorkWithCompression();      
    }

    static void WorkWithText()
    {
        string textFile = Combine(dir, "streams.txt");

        StreamWriter text = File.CreateText(textFile);

        foreach (string item in Viper.Callsigns)
        {
            text.WriteLine(item);
        }
        text.Close();

        // выводим содержимое файла
        WriteLine("{0} contains {1:N0} bytes.",
        arg0: textFile,
        arg1: new FileInfo(textFile).Length);
        WriteLine(File.ReadAllText(textFile));
    }

    static void WorkWithXml()
    {
        //FileStream? xmlFileStream = null;
        //XmlWriter? xml = null;


        //// освобождение неконтролируемых ресурсов 
        //try
        //{
        //    string xmlFile = Combine(dir, "streams.xml");
        //    xmlFileStream = File.Create(xmlFile);

        //    // оборачиваем файловый поток во вспомогательную функцию XML
        //    // и создаем автоматический отступ для вложенных элементов
        //    xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

        //    xml.WriteStartDocument(); // пишем XML-декларацию
        //    xml.WriteStartElement("callsings"); // записываем корневой элемент

        //    // перечисляем строки, записывая каждую в поток
        //    foreach (string item in Viper.Callsigns)
        //    {
        //        xml.WriteElementString("callsign", item);
        //    }

        //    // записываем закрывающий корневой элемент
        //    xml.WriteEndElement();

        //    // закрываем вспомогательную функцию и поток
        //    xml.Close();
        //    xmlFileStream.Close();

        //    // выводим все содержимое файла
        //    WriteLine("{0} contains {1:N0} bytes.",
        //    arg0: xmlFile,
        //    arg1: new FileInfo(xmlFile).Length);
        //    WriteLine(File.ReadAllText(xmlFile));
        //}
        //catch(Exception ex)
        //{
        //    WriteLine($"{ex.GetType()} says {ex.Message}");
        //}
        
        // освобождение ресурсов
        //finally
        //{
        //    if(xml != null)
        //    {
        //        xml.Dispose();
        //        WriteLine("XML writers resources have been dispodes");
        //    }
        //    if(xmlFileStream != null)
        //    {
        //        xmlFileStream.Close();
        //        WriteLine("The file streams resources have been disposed");
        //    }
        //}


        // упрощение освобождения
        string xmlFile = Combine(dir, "file2.xml");
        using FileStream? file2 = File.Create(xmlFile);
        using XmlWriter writer2 = XmlWriter.Create(file2, new XmlWriterSettings { Indent = true });

        try
        {
            writer2.WriteStartDocument(); // пишем XML-декларацию
            writer2.WriteStartElement("callsings"); // записываем корневой элемент
            foreach (string item in Viper.Callsigns)
            {
                writer2.WriteElementString("callsign", item);
            }

            // записываем закрывающий корневой элемент
            writer2.WriteEndElement();

            // закрываем вспомогательную функцию и поток
            writer2.Close();
            file2.Close();

            // выводим все содержимое файла
            WriteLine("{0} contains {1:N0} bytes.",
            arg0: file2,
            arg1: new FileInfo(xmlFile).Length);
            WriteLine(File.ReadAllText(xmlFile));
        }
        catch(Exception ex)
        {
            WriteLine($"{ex.GetType()} says {ex.Message}");
        }
        
    }
    static void WorkWithCompression()
    {
        string fileExt = "gzip";

        // сжимаем XML-вывод
        string filePath = Combine(
        dir, $"streams.{fileExt}");

        FileStream file = File.Create(filePath);
        Stream compressor = new GZipStream(file, CompressionMode.Compress);

        using (compressor)
        {
            using (XmlWriter xml = XmlWriter.Create(compressor))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");
                foreach (string item in Viper.Callsigns)
                {
                    xml.WriteElementString("callsign", item);
                }
                // обычный вызов WriteEndElement не требуется,
                // поскольку, освобождая неуправляемые ресурсы,
                // используемые объектом XmlWriter,
                // автоматически завершает любые элементы любой глубины
            }
        } // также закрывает базовый поток

        WriteLine("{0} contains {1:N0} bytes.",filePath, new FileInfo(filePath).Length);
        WriteLine($"The compressed contents:");
        WriteLine(File.ReadAllText(filePath));

        // чтение сжатого файла
        WriteLine("Reading the compressed XML file:");
        file = File.Open(filePath, FileMode.Open);

        // распаковываем файл
        Stream decompressor = new GZipStream(file,CompressionMode.Decompress);
        using (decompressor)
        {
            using (XmlReader reader = XmlReader.Create(decompressor))
            {
                while (reader.Read()) // чтение следующего XML-узла
                {
                    // проверяем, находимся ли мы на узле элемента с именем позывной
                    if ((reader.NodeType == XmlNodeType.Element)
                    && (reader.Name == "callsign"))
                    {
                        reader.Read(); // переходим к тексту внутри элемента
                        WriteLine($"{reader.Value}"); // читаем его значение
                    }
                }
            }
        }
        WriteLine("{0} contains {1:N0} bytes.", filePath, new FileInfo(filePath).Length);
    }
}

internal static class Viper
{
  public static string[] Callsigns = new[]
  {
      "Husker", "Starbuck", "Apollo", "Boomer",
      "Bulldog", "Athena", "Helo", "Racetrack"
  };
}
