using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace MediaFileInfo
{
    class MediaFile
    {
        [Key]
        public int MediaID { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Duration { get; set; }
        public void Print() => Console.WriteLine($"Name: {Name} / Size: {Size} / Duration: {Duration}"); 
    }
}
