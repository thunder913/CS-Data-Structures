namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        private Tree<int> Root { get; set; }

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            var root = int.Parse(input[0].Split()[0].ToString());
            var rootNode = this.CreateNodeByKey(root);
            this.Root = rootNode;
            this.nodesBykeys.Add(root, rootNode);

            for (int i = 0; i < input.Length; i++)
            {
                var parent = int.Parse(input[i].Split()[0].ToString());
                var child = int.Parse(input[i].Split()[1].ToString());
                this.AddEdge(parent, child);
            }

            return this.GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            return new Tree<int>(key);
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = this.nodesBykeys[parent];
            var childNode = this.CreateNodeByKey(child);
            childNode.AddParent(parentNode);
            parentNode.AddChild(childNode);
            this.nodesBykeys.Add(child, childNode);
        }

        private Tree<int> GetRoot()
        {
            return this.Root;
        }
    }
}
