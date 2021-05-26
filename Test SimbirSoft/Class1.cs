using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Test_SimbirSoft
{
    class Class1
    {
        public static void Analiz()
        {
            //Вывод в консоль запроса
            Console.WriteLine("Введите адресс HTML страницы, для скачивания (пример сайта www.simbirsoft.ru)");
            
            string adr = Console.ReadLine();

            
            WebClient webclient = new WebClient();
            //Скачивание и преобразование в байты, HTML страницы
            byte[] myData = webclient.DownloadData("http://"+adr);
           
            //Перешифровка в стринг (кодировка UTF8)
            string markup = Encoding.UTF8.GetString(myData);

            //Создание текстового документа и проверка на сущ. документ если уже есть то пересоздать
            string path = @"c:\temp\MyTest.txt";

            using (FileStream fs = File.Create(path))

            {
                // Записываем информацию в файл
                fs.Write(myData, 0, myData.Length);
            }

            //Создание списка розделителей (взят из тестового заадния)
            char[] symbol = new char[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
            string[] words = markup.Split(symbol, StringSplitOptions.RemoveEmptyEntries);

            //Проверка на повтор и вывод количества повторений
            var result = words.GroupBy(x => x)
                              .Where(x => x.Count() > 1)
                              .Select(x => new { Word = x.Key, Frequency = x.Count() });

            foreach (var item in result)

            {
                                            
                Console.WriteLine("Слово: {0}\tКоличество повторов: {1}", item.Word, item.Frequency);
            }

            
        }


    }
}
