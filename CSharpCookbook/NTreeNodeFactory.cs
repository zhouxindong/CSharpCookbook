using System;

namespace CSharpCookbook
{
    public class NTreeNodeFactory<T>
        where T : IComparable<T>
    {
        private int _max_children = 0;
        public NTreeNodeFactory(NTree<T> root)
        {
            _max_children = root.MaxChildren;
        }

        public int MaxChildren
        {
            get { return _max_children; }
        }

        public NTreeNode<T> CreateNode(T value)
        {
            return new NTreeNode<T>(value, _max_children);
        }
    }
}