namespace Final_Project
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Final_Project : DbContext
	{
		public Final_Project()
			: base("name=FinalProject")
		{
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
