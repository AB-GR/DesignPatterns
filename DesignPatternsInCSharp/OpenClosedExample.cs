namespace DesignPatternsInCSharp
{
	internal class OpenClosedExample
	{
		public void Run()
		{
			var apple = new Product { Name = "Apple", Description = "Fruit", Color = Color.Red, Size = Size.Small };
			var tree = new Product { Name = "Tree", Description = "Christmas Tree", Color = Color.Green, Size = Size.Medium };
			var house = new Product { Name = "House", Description = "A huge house", Color = Color.Yellow, Size = Size.Large };
			var products = new[] { apple, tree, house };
			var filter = new ProductFilter();
			foreach (var item in filter.FilterByColor(products, Color.Green))
			{
				Console.WriteLine(item.Name + " " + item.Color);
			}

			var filterWithSpec = new ProductFilterWithSpec();
			foreach (var item in filterWithSpec.Filter(products, new ColorSpecification(Color.Red)))
			{
				Console.WriteLine(item.Name + " " + item.Color);
			}

			foreach (var item in filterWithSpec.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Yellow), new SizeSpecification(Size.Large))))
			{
				Console.WriteLine(item.Name + " " + item.Color);
			}
		}
	}

	public interface ISpecification<T>
	{
		bool IsSatisfied(T t);
	}

	public interface IFilter<T>
	{
		IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
	}

	public class AndSpecification<T> : ISpecification<T>
	{
		private readonly ISpecification<T> first;
		private readonly ISpecification<T> second;

		public AndSpecification(ISpecification<T> first, ISpecification<T> second)
		{
			this.first = first;
			this.second = second;
		}

		public bool IsSatisfied(T t) 
			=> this.first.IsSatisfied(t) && this.second.IsSatisfied(t);
	}

	public class ColorSpecification : ISpecification<Product>
	{
		private Color color;

		public ColorSpecification(Color color)
		{
			this.color = color;
		}

		public bool IsSatisfied(Product t) => t.Color == color;
	}

	public class SizeSpecification : ISpecification<Product>
	{
		private Size size;

		public SizeSpecification(Size size)
		{
			this.size = size;
		}

		public bool IsSatisfied(Product t) => t.Size == size;
	}

	public class ProductFilterWithSpec : IFilter<Product>
	{
		public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> specification)
		{
			foreach (var product in products)
			{
				if (specification.IsSatisfied(product))
					yield return product;
			}
		}
	}

	public class ProductFilter
	{
		public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
		{
			foreach (var product in products)
			{
				if(product.Color == color)
					yield return product;
			}
		}
	}

	public enum Color
	{
		Red, Green, Yellow
	}

	public enum Size
	{
		Small, Medium, Large
	}

	public class Product
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public Color Color { get; set; }

		public Size Size { get; set; }
	}
}
