﻿namespace Bandodientu.Models
{
    public class Cart
    {
		//public int MaHh { get; set; }
		//public string? TenHH { get; set; }
		//public string? Hinh { get; set; }
		//public decimal DonGia { get; set; }
		//public int SoLuong { get; set; }

		public List<CartLine> Lines { get; set; } = new List<CartLine>();
		public void AddItem(Product product, int quanlity)
		{
			CartLine? line = Lines
				.Where(p => p.Product.ProductID == product.ProductID)
				.FirstOrDefault();
			if (line == null)
			{
				Lines.Add(new CartLine
				{
					Product = product,
					Quantity = quanlity
				});
			}
			else
			{
				line.Quantity += quanlity;
			}
		}
		public void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
		public decimal ComputeTotalValue() => Lines.Sum(e => e.Product.DiscountedPrice * e.Quantity);
		public void Clear() => Lines.Clear();
	}
	public class CartLine
	{
		public int CartLineID { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}