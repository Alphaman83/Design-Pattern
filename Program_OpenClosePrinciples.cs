using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, XLarge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;

        }
    }
    public class ProductFilter
    {
        /// <summary>
        /// FilterBy Size
        /// </summary>
        /// <param name="products"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products,
            Size size)
        {
            foreach (var item in products)
            {
                if (item.Size == size)
                    yield return item;

            }
        }
        /// <summary>
        /// FilterBy Color
        /// </summary>
        /// <param name="products"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products,
            Color color)
        {
            foreach (var item in products)
            {
                if (item.Color == color)
                    yield return item;

            }
        }

        /// <summary>
        /// Filter By Size and Color
        /// </summary>
        /// <param name="products"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products,
            Size size, Color color)
        {
            foreach (var item in products)
            {
                if (item.Size == size && item.Color == color)
                    yield return item;

            }
        }
    }
    #region "Interface Implementation on Filters"

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
   
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class AndSpecification<T>:ISpecification<T>
    {
        private ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first,ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(
                                            IEnumerable<Product> items,
                                            ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;

        }
    }

    #endregion
    public class Program_OpenClosePrinciples
    {
        static void Main(string[] args)
        {
            WriteLine("Bismillah!");
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var toycar = new Product("Toy Car", Color.Blue, Size.Large);
            var luckybamboo = new Product("Lucky Bamboo", Color.Green, Size.Large);

            Product[] products = { apple, tree, toycar, luckybamboo };
            #region "Product Filter Normal"
            var pf = new ProductFilter();
            WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(products,Color.Green))
            {
                WriteLine($"-{p.Name} is Green.");
            }
            #endregion
            WriteLine('\n');
            #region "Better Filter"
            var bf = new BetterFilter();
            WriteLine("Green products (new)");
            foreach (var i in bf.Filter(products, new ColorSpecification(Color.Green)))
                WriteLine($"-{i.Name} is Green.");
            WriteLine('\n');

            WriteLine("Large blue Products");
            foreach (var i in bf.Filter(
                products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large))))
            {
                WriteLine($"-{i.Name} is Blue Product.");
            }
            #endregion

            ReadLine();
        }
    }
}
