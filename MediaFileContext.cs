using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaFileInfo
{
	public class MediaFileContext : DbContext
	{
		public DbSet<MediaFile> Media { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MediaInfos;Trusted_Connection=True;");
		}
	}
}
