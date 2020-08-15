using System;
using System.Collections.Generic;
using System.IO;
namespace MediaFileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MediaFile> list = new List<MediaFile>();

            if (!File.Exists(@"..\..\..\MediaFileInfos.txt")){
                Console.WriteLine("File not found");
            }
            string[] readText = File.ReadAllLines(@"..\..\..\MediaFileInfos.txt");
            for (int i = 0; i < readText.Length; i++)
            {
                if (readText[i] == "Name;Size(bytes);Duration(seconds)")
                    continue;
                string[] arr = readText[i].Split(';');
                list.Add(new MediaFile() { Name = arr[0], Duration = Convert.ToInt32(arr[1]), Size = Convert.ToInt32(arr[2]) });
            }

            foreach(var k in list)
            {
                k.Print();
            }

            MediaFilesManager MFM = new MediaFilesManager();
            //MFM.Import(list);

            MFM.GetAll();

            DataProcessor data = new DataProcessor();
            //data.ToJson(@"..\..\..\Example.json", list);


            Console.WriteLine("From Json: ");
            foreach(var k in data.FromJson(@"..\..\..\Example.json"))
            {
                Console.WriteLine($"Name: {k.Name} / Duration: {k.Duration} / Size: {k.Size}");
            }

            MFM.Update(list[0]);
        }
    }
} 
