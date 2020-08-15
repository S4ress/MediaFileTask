using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MediaFileInfo
{
    class MediaFilesManager : DbContext
    {
        public DbSet<MediaFile> Media { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;");
        }
        public void Import(IEnumerable<MediaFile> k)
        {
            List<MediaFile> temp = new List<MediaFile>();
            temp = k.ToList();
            using (var context = new MediaFilesManager())
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    var std = new MediaFile()
                    {
                        Name = temp[i].Name,
                        Duration = temp[i].Duration,
                        Size = temp[i].Size
                    };

                    context.Media.Add(std);
                    context.SaveChanges();
                }
            }
        }

        public void GetAll()
        {
            string queryString = "SELECT Name, Size, Duration FROM dbo.Media;";

            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("From DB:");

                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader[0]} / Size: {reader[1]} / Duration: {reader[2]}");
                }

                reader.Close();
            }
        }

        public void Update(MediaFile mediaFile)
        {
            if(mediaFile.MediaID == 0)
            {
                using (var context = new MediaFilesManager())
                {
                    var std = new MediaFile()
                    {
                        Name = mediaFile.Name,
                        Duration = mediaFile.Duration,
                        Size = mediaFile.Size
                    };
                    context.Media.Add(std);
                    context.SaveChanges();
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;"))
                    using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE dbo.Media(Name, Size, Duration) VALUES (@name, @size, @duration) WHERE id = " + mediaFile.MediaID;
                    command.Parameters.AddWithValue("@name", mediaFile.Name);
                    command.Parameters.AddWithValue("@size", mediaFile.Size);
                    command.Parameters.AddWithValue("@duration", mediaFile.Duration);

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
