using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCookbook
{
    public class NTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        protected NTreeNode<T> root_ = null;
        protected int max_children_ = 0;

        public NTree()
        {
            max_children_ = int.MaxValue;
        }

        public NTree(int max_children)
        {
            max_children_ = max_children;
        }

        public void AddRoot(NTreeNode<T> node)
        {
            root_ = node;
        }

        public NTreeNode<T> GetRoot()
        {
            return root_;
        }

        public int MaxChildren
        {
            get { return max_children_; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var nodes = new List<T>();
            nodes = GetRoot().IterateDepthFirst();
            nodes.Add(GetRoot().Value());

            foreach (var node in nodes)
                yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException("This operation is not " +
                "supported use the GetEnumerator() that returns an IEnumerator<T>.");
        }
    }
}