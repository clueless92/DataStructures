using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr01PlayWithTrees
{
    class Tree<T>
    {
        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (Tree<T> child in children)
            {
                this.Children.Add(child);
                child.Parrent = this;
            }
        }

        public T Value { get; set; }

        public Tree<T> Parrent { get; set; }

        public IList<Tree<T>> Children { get; set; }
    }
}
