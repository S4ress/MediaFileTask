using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MediaFileInfo
{
    class DataProcessor
    {
        public void ToJson(string path, IEnumerable<MediaFile> mediaFile)
        {
            string json = JsonConvert.SerializeObject(mediaFile, Formatting.Indented);
            System.IO.File.WriteAllText($@"{path}", json);
        }

        public IEnumerable<MediaFile> FromJson(string path)
        {
            try {
                var json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<IEnumerable<MediaFile>>(json);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found");
                return Enumerable.Empty<MediaFile>();
            }
        }
    }
}
