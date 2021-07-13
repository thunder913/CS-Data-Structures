using System;
using Tree;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = new string[]
{
                "7 19",
                "7 21",
                "7 14",
                "19 1",
                "19 12",
                "19 31",
                "14 23",
                "14 6",
                "6 99",
                "99 91",
                "6 92",
                "92 93",
                "93 94"
};

            var _treeFactory = new TreeFactory();
            var _tree = _treeFactory.CreateTreeFromStrings(input);
            var node = _tree.GetDeepestLeftomostNode();

            Console.WriteLine(node.Key);
        }
    }
}
