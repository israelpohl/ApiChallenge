using System;
using System.Linq;
using ProductManager.Core.Domain;

namespace ProductManager.Core.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ProductDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Products.Any())
			{
				return;
			}
			var products = new Product[]
			{
				new Product{Name="Trombone", Category="Instruments"},
				new Product{Name="Violin", Category="Instruments"},
				new Product{Name="Guitar", Category="Instruments"},
				new Product{Name="Piano", Category="Instuments"},
				new Product{Name="Guitar strings", Category="Accessoires"},
				new Product{Name="Guitar stand", Category="Accessoires"},
				new Product{Name="Violing strings", Category="Accessoires"}
			};
			foreach(Product p in products)
            {
				context.Products.Add(p);
            }
			context.SaveChanges();
		}
	}
}

