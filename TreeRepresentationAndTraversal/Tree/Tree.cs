namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree()
        {
        }


        public Tree(T key)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();
        }

        public Tree(T key, Tree<T> parent)
        {
            this.Key = key;
            this.Parent = parent;
            this._children = new List<Tree<T>>();
        }

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            foreach (var child in children)
            {
                this.AddChild(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            return DFS(this, new StringBuilder(), 0);
        }

        private string DFS(Tree<T> node, StringBuilder sb, int level)
        {
            sb.Append(new string(' ', level));
            sb.AppendLine(node.Key.ToString());

            foreach (var child in node.Children)
            {
                DFS(child, sb, level + 2);
            }

            return sb.ToString().TrimEnd();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafes = this.GetLeafNodes(this, new List<Tree<T>>());
            var longest = 0;
            Tree<T> deepestNode = null;
            for (int i = 0; i < leafes.Count; i++)
            {
                var leaf = leafes[i];
                var current = 0;
                while (leaf.Parent != null)
                {
                    leaf = leaf.Parent;
                    current++;
                }
                if (current > longest)
                {
                    longest = current;
                    deepestNode = leafes[i];
                }
            }

            return deepestNode;
        }

        public List<T> GetLeafKeys()
        {
            var nodes = this.GetLeafNodes(this, new List<Tree<T>>());
            var keys = nodes.Select(x => x.Key).OrderBy(x => x).ToList();
            return keys;
        }

        private List<Tree<T>> GetLeafNodes(Tree<T> node, List<Tree<T>> items)
        {
            if (!node.Children.Any())
            {
                items.Add(node);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    GetLeafNodes(child, items);
                }
            }

            return items;
        }

        public List<T> GetMiddleKeys()
        {
            var nodes = this.GetMiddleNodes(this, new List<Tree<T>>());
            var keys = nodes.Select(x => x.Key).OrderBy(x => x).ToList();
            return keys;
        }

        private List<Tree<T>> GetMiddleNodes(Tree<T> node, List<Tree<T>> items)
        {
            if (node.Children.Any() && node.Parent != null)
            {
                items.Add(node);
            }
            
            if (node.Children.Any())
            {
                foreach (var child in node.Children)
                {
                    GetMiddleNodes(child, items);
                }
            }
            return items;
        }

        public List<T> GetLongestPath()
        {
            var leafes = this.GetLeafNodes(this, new List<Tree<T>>());
            var longest = 0;
            Tree<T> deepestNode = null;
            for (int i = 0; i < leafes.Count; i++)
            {
                var leaf = leafes[i];
                var current = 0;
                while (leaf.Parent != null)
                {
                    leaf = leaf.Parent;
                    current++;
                }
                if (current > longest)
                {
                    longest = current;
                    deepestNode = leafes[i];
                }
            }

            var toReturn = new List<T>();
            while (deepestNode != null && deepestNode != null)
            {
                toReturn.Add(deepestNode.Key);
                deepestNode = deepestNode.Parent;
            }
            toReturn.Reverse();
            return toReturn;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var toReturn = new List<List<T>>();
            var nodes = this.GetNodesWithSum(sum);
            foreach (var node in nodes)
            {
                var currentPath = new List<T>();
                var currentNode = node;
                while (currentNode.Parent != null)
                {
                    currentPath.Add(currentNode.Key);
                    currentNode = currentNode.Parent;
                }
                currentPath.Add(currentNode.Key);
                currentPath.Reverse();
                toReturn.Add(currentPath);
            }
            return toReturn;
        }

        private List<Tree<T>> GetNodesWithSum(int sum)
        {
            var leafs = this.GetLeafNodes(this, new List<Tree<T>>());
            var nodes = new List<Tree<T>>();
            for (int i = 0; i < leafs.Count; i++)
            {
                var leaf = leafs[i];
                int currentSum = 0;
                while (leaf.Parent != null)
                {
                    currentSum += int.Parse(leaf.Key.ToString());
                    leaf = leaf.Parent;
                }
                currentSum += int.Parse(leaf.Key.ToString());
                if (currentSum == sum)
                {
                    nodes.Add(leafs[i]);
                }
            }
            return nodes;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            return GetSumRecursive(this, sum, new List<Tree<T>>());
        }

        private List<Tree<T>> GetSumRecursive(Tree<T> node, int sum, List<Tree<T>> nodes)
        {
            var nodeSum = GetSubtreeSum(node, 0);
            if (sum == nodeSum)
            {
                nodes.Add(node);
            }

            if (node.Children.Any())
            {
                foreach (var child in node.Children)
                {
                    GetSumRecursive(child, sum, nodes);
                }
            }

            return nodes;
        }

        private int GetSubtreeSum(Tree<T> node, int sum)
        {
            var currentSum = sum + int.Parse(node.Key.ToString());
            if (node.Children.Any())
            {
                foreach (var child in node.Children)
                {
                    currentSum = GetSubtreeSum(child, currentSum);
                }
            }

            return currentSum;
        }
    }
}
