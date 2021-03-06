﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace TestJob_v2
{
    class Dictionary
    {
        public List<string> readedwords;            //считанные слова из файла
        public List<string> selection;              //выборка из 5 слов
        string request { get; set; }
         
        public Dictionary(string path, string request)
        {
            string[] input = File.ReadAllLines(path, Encoding.GetEncoding(1251));
            readedwords = new List<string>(input);
            selection = new List<string>();
            this.request = request;
        }
        public IEnumerable<string> Filling()       //выбираем все подходящие под запрос слова
        {
            readedwords.Sort();
            int i = readedwords.BinarySearch(request);           //бинарный поиск
            if (i < 0)
                i = -i - 1;
            while (i >= 0 && readedwords[i].StartsWith(request))         //если вернули не самое первое соответсвие запросу
                --i;                                                //поднимаемся наверх, к самому первому
            ++i;
            while (i < readedwords.Count && readedwords[i].StartsWith(request))      //возвращаем все слова, соответствующие нашему запросу
            {
                yield return readedwords[i];
                ++i;
            }

        }
        public void Complete()            //выбираем 5 лучших
        {               
            var suitable = Filling();
            foreach (var str in suitable)
            {
                if (selection.Count >= 5)
                    break;
                selection.Add(str);                   //т.к слова отсортированы, выбираем 5 первых из всех
                Console.WriteLine(str);
            }
            if (selection.Count == 0)
                Console.WriteLine("No matches found");
        }
    }
}
