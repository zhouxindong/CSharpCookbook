using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCookbook
{
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        protected int counter_ = 0;
        protected BinaryTreeNode<T> root_ = null;

        public BinaryTree() { }

        public BinaryTree(T value)
        {
            var node = new BinaryTreeNode<T>(value);
            root_ = node;
            counter_ = 1;
        }

        public void AddNode(T value)
        {
            var node = new BinaryTreeNode<T>(value);
            ++counter_;
            if (root_ == null)
                root_ = node;
            else 
                root_.AddNode(node);
        }

        public BinaryTreeNode<T> SearchDepthFirst(T value)
        {
            return root_.DepthFirstSearch(value);
        }

        public string Print()
        {
            return root_.PrintDepthFirst();
        }

        public BinaryTreeNode<T> Root
        {
            get { return root_; }
        }

        public int Count
        {
            get { return counter_; }
        }

        IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            var nodes = new List<T>();
            nodes = root_.IterateDepthFirst();
            nodes.Add(root_.Value);
            foreach (var item in nodes)
            {
                yield return item;
            }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException("This operation is not " + 
                "supported use the GetEnumerator() that returns an IEnumerator<T>.");
        }

    }
}