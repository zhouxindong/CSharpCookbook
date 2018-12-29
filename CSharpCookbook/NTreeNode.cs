using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCookbook
{
    public class NTreeNode<T>
        where T : IComparable<T>
    {
        protected T node_value_ = default(T);
        protected NTreeNode<T>[] child_nodes_ = null;

        public NTreeNode(T value, int max_children)
        {
            if (value != null)
                node_value_ = value;
            child_nodes_ = new NTreeNode<T>[max_children];
        }

        public int CountChildren
        {
            get
            {
                int cur_count = 0;
                for (int i = 0; i <= child_nodes_.GetUpperBound(0); i++)
                {
                    if (child_nodes_[i] != null)
                    {
                        ++cur_count;
                        cur_count += child_nodes_[i].CountChildren;
                    }
                }
                return cur_count;
            }
        }

        public int CountImmediateChildren
        {
            get
            {
                int cur_count = 0;
                for (int i = 0; i <= child_nodes_.GetUpperBound(0); i++)
                {
                    if (child_nodes_[i] != null)
                        ++cur_count;
                }
                return cur_count;
            }
        }

        public NTreeNode<T>[] Children
        {
            get { return child_nodes_; }
        }

        public NTreeNode<T> GetChild(int index)
        {
            return child_nodes_[index];
        }

        public T Value()
        {
            return node_value_;
        }

        public void AddNode(NTreeNode<T> node)
        {
            var childs = CountImmediateChildren;
            if (childs < child_nodes_.Length)
            {
                child_nodes_[childs] = node;
            }
            else
            {
                throw new Exception("Cannot add more children to this node.");
            }
        }

        public NTreeNode<T> DepthFirstSearch(T target_obj)
        {
            NTreeNode<T> ret_obj = default(NTreeNode<T>);
            if (target_obj.CompareTo(node_value_) == 0)
                ret_obj = this;
            else
            {
                for (int i = 0; i <= child_nodes_.GetUpperBound(0); i++)
                {
                    if (child_nodes_[i] != null)
                    {
                        ret_obj = child_nodes_[i].DepthFirstSearch(target_obj);
                        if (ret_obj != null) break;
                    }
                }
            }
            return ret_obj;
        }

        public NTreeNode<T> BreadthFirstSearch(T target_obj)
        {
            Queue<NTreeNode<T>> row = new Queue<NTreeNode<T>>();
            row.Enqueue(this);

            while (row.Count > 0)
            {
                NTreeNode<T> cur_node = row.Dequeue();
                if (target_obj.CompareTo(cur_node.node_value_) == 0)
                {
                    return cur_node;
                }

                for (int i = 0; i < cur_node.CountImmediateChildren; i++)
                {
                    if (cur_node.Children[i] != null)
                    {
                        row.Enqueue(cur_node.Children[i]);
                    }
                }
            }
            return null;
        }

        public string PrintDepthFirst()
        {
            var strbld = new StringBuilder();
            strbld.AppendLine("this: " + node_value_.ToString());

            for (int i = 0; i < child_nodes_.Length; i++)
            {
                if (child_nodes_[i] != null)
                {
                    strbld.AppendLine("\tchildNodes[" + i + "]: " + child_nodes_[i].node_value_.ToString());
                }
                else
                {
                    strbld.AppendLine("\tchildNodes[" + i + "]: NULL");
                }
            }
            for (int i = 0; i < child_nodes_.Length; i++)
            {
                if (child_nodes_[i] != null)
                {
                    return child_nodes_[i].PrintDepthFirst();
                }
            }

            return strbld.ToString();
        }

        public List<T> IterateDepthFirst()
        {
            List<T> temp_list = new List<T>();
            for (int i = 0; i < child_nodes_.Length; i++)
            {
                if (child_nodes_[i] != null)
                {
                    temp_list.Add(child_nodes_[i].node_value_);
                }
            }
            for (int i = 0; i < child_nodes_.Length; i++)
            {
                if (child_nodes_[i] != null)
                {
                    temp_list.AddRange(child_nodes_[i].IterateDepthFirst());
                }
            }

            return temp_list;
        }

        public void RemoveNode(int index)
        {
            if (index < child_nodes_.GetLowerBound(0) || index > child_nodes_.GetUpperBound(0))
            {
                throw new ArgumentOutOfRangeException("index", index, "Array index out of bounds.");
            }
            else if (index < child_nodes_.GetUpperBound(0))
            {
                Array.Copy(child_nodes_, index + 1, child_nodes_, index, child_nodes_.Length - index - 1);
            }
            child_nodes_.SetValue(null, child_nodes_.GetUpperBound(0));
        }
    }
}