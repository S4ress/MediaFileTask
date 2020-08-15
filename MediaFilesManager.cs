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
    public class MediaFilesManager
    {
		public void Import(IEnumerable<MediaFile> medias)
		{
			using (var context = new MediaFileContext())
			{
				context.Media.AddRange(medias);
				context.SaveChanges();
			}
		}

		public IEnumerable<MediaFile> GetAll()
		{
			using (var context = new MediaFileContext())
			{
				return context.Media.AsNoTracking().ToList();
			}
			//string queryString = "SELECT Name, Size, Duration FROM dbo.Media;";

			//using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;"))
			//{
			//    SqlCommand command = new SqlCommand(queryString, connection);
			//    connection.Open();

			//    SqlDataReader reader = command.ExecuteReader();

			//    Console.WriteLine("From DB:");

			//    while (reader.Read())
			//    {
			//        Console.WriteLine($"Name: {reader[0]} / Size: {reader[1]} / Duration: {reader[2]}");
			//    }

			//    reader.Close();
			//}
		}

		public void Update(MediaFile mediaFile)
		{
			if (mediaFile.MediaID == 0)
			{
				using (var context = new MediaFileContext())
				{
					context.Media.Add(mediaFile);
					context.SaveChanges();
				}
			}
			else
			{

				//// attached
				//using (var context = new MediaFileContext())
				//{
				//	var m = context.Media.FirstOrDefault(x => x.MediaID == mediaFile.MediaID);
				//	if (m != null)
				//	{
				//		m.Duration = mediaFile.Duration;
				//		m.Size = mediaFile.Size;
				//	}
				//	context.SaveChanges();
				//}

				// detached
				using (var context = new MediaFileContext())
				{
					var m = new MediaFile() { MediaID = mediaFile.MediaID };
					var entry = context.Attach<MediaFile>(m);
					entry.State = EntityState.Unchanged;
					m = entry.Entity;
					m.Duration = mediaFile.Duration;
					m.Size = mediaFile.Size;
					context.SaveChanges();
				}

					//using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;"))
					//using (SqlCommand command = connection.CreateCommand())
					//{
					//	command.CommandText = "UPDATE dbo.Media(Name, Size, Duration) VALUES (@name, @size, @duration) WHERE id = " + mediaFile.MediaID;
					//	command.Parameters.AddWithValue("@name", mediaFile.Name);
					//	command.Parameters.AddWithValue("@size", mediaFile.Size);
					//	command.Parameters.AddWithValue("@duration", mediaFile.Duration);

					//	connection.Open();

					//	command.ExecuteNonQuery();

					//	connection.Close();
					//}
			}
		}

		//public class MediaFileProxy : MediaFile
		//{
		//	private bool durationChanged = false;
		//	public int Duration
		//	{
		//		get
		//		{
		//			return base.Duration;
		//		}
		//		set
		//		{
		//			base.Duration = value;
		//			durationChanged = true;
		//		}
		//	}
		//}
	}
}
