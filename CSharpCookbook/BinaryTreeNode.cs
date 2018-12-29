using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCookbook
{
    /// <summary>
    /// 二叉树单个节点的数据和操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTreeNode<T>
        where T : IComparable<T>
    {
        protected T NodeValue = default(T);
        protected BinaryTreeNode<T> LeftNode = null;
        protected BinaryTreeNode<T> RightNode = null;

        public BinaryTreeNode() { }

        public BinaryTreeNode(T value)
        {
            NodeValue = value;
        }

        public int Children
        {
            get
            {
                int cur_count = 0;
                if (LeftNode != null)
                {
                    ++cur_count;
                    cur_count += LeftNode.Children;
                }
                if (RightNode != null)
                {
                    ++cur_count;
                    cur_count += RightNode.Children;
                }
                return cur_count;
            }
        }

        public BinaryTreeNode<T> Left
        {
            get { return LeftNode; }
        }

        public BinaryTreeNode<T> Right
        {
            get { return RightNode; }
        }

        public T Value
        {
            get { return NodeValue; }
        }

        public void AddNode(BinaryTreeNode<T> node)
        {
            if (node.NodeValue.CompareTo(NodeValue) < 0)
            {
                if (LeftNode == null)
                    LeftNode = node;
                else
                    LeftNode.AddNode(node);
            }
            else if (node.NodeValue.CompareTo(NodeValue) >= 0)
            {
                if (RightNode == null)
                    RightNode = node;
                else 
                    RightNode.AddNode(node);
            }
        }

        public bool AddUniqueNode(BinaryTreeNode<T> node)
        {
            bool is_unique = true;
            if (node.NodeValue.CompareTo(NodeValue) < 0)
            {
                if (LeftNode == null)
                    LeftNode = node;
                else 
                    LeftNode.AddNode(node);
            }
            else if (node.NodeValue.CompareTo(NodeValue) > 0)
            {
                if (RightNode == null)
                    RightNode = node;
                else
                    RightNode.AddNode(node);
            }
            else
            {
                is_unique = false;
            }
            return is_unique;
        }

        public BinaryTreeNode<T> DepthFirstSearch(T target_obj)
        {
            BinaryTreeNode<T> ret_obj = null;
            int comparison_result = target_obj.CompareTo(NodeValue);
            if (comparison_result == 0)
            {
                ret_obj = this;
            }
            else if (comparison_result > 0)
            {
                if (RightNode != null)
                {
                    ret_obj = RightNode.DepthFirstSearch(target_obj);
                }
            }
            else if (comparison_result < 0)
            {
                if (LeftNode != null)
                {
                    ret_obj = LeftNode.DepthFirstSearch(target_obj);
                }
            }
            return ret_obj;
        }

        public string PrintDepthFirst()
        {
            var strbld = new StringBuilder();
            if (LeftNode != null)
            {
                return LeftNode.PrintDepthFirst();
            }
            strbld.AppendLine(this.NodeValue.ToString());

            if (LeftNode != null)
            {
                strbld.AppendLine("\tContains Left: " + LeftNode.NodeValue.ToString());
            }
            else
            {
                strbld.AppendLine("\tContains Left: NULL");
            }
            if (RightNode != null)
            {
                strbld.AppendLine("\tContains Right: " + RightNode.NodeValue.ToString());
            }
            else
            {
                strbld.AppendLine("\tContains Right: NULL");
            }
            if (RightNode != null)
            {
                return RightNode.PrintDepthFirst();
            }
            return strbld.ToString();
        }

        public List<T> IterateDepthFirst()
        {
            List<T> temp_list = new List<T>();
            if (LeftNode != null)
            {
                temp_list.AddRange(LeftNode.IterateDepthFirst());
            }

            if (LeftNode != null)
            {
                temp_list.Add(LeftNode.NodeValue);
            }

            if (RightNode != null)
            {
                temp_list.Add(RightNode.NodeValue);
            }
            if (RightNode != null)
            {
                temp_list.AddRange(RightNode.IterateDepthFirst());
            }

            return temp_list;
        }

        public void RemoveLeftNode()
        {
            LeftNode = null;
        }

        public void RemoveRightNode()
        {
            RightNode = null;
        }
    }
}