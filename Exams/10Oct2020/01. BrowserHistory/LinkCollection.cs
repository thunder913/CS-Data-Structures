using _01._BrowserHistory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _01._BrowserHistory
{
    public class LinkCollection
    {
        public LinkCollection(Node<ILink> head = null)
        {
            this._head = head;
        }

        private int _count { get; set; }

        public int Size => this._count;

        private Node<ILink> _head { get; set; }
        private Node<ILink> _tail { get; set; }

        public void Add(ILink link)
        {
            if (this.Size == 0)
            {
                this._count++;
                this._head = new Node<ILink>(link);
                this._tail = this._head;
                return;
            }
            var newLink = new Node<ILink>(link);
            newLink.Next = this._head;
            this._head = newLink;
            this._head.Next.Previous = this._head;
            this._count++;
        }

        public ILink DeleteFirst()
        {
            var removed = this._head.Value;
            this._head = this._head.Next;
            this._count--;
            return removed;
        }

        public ILink DeleteLast()
        {
            var removed = this._tail.Value;
            this._tail = this._tail.Previous;
            this._count--;
            return removed;
        }

        public List<ILink> GetAllLinks()
        {
            var links = new List<ILink>();
            var node = this._head;
            while (node != null)
            {
                links.Add(node.Value);
                node = node.Next;
            }

            return links;
        }

        public void Clear()
        {
            this._head = null;
            this._tail = null;
            this._count = 0;
        }

        public ILink GetElement(ILink link)
        {
            var node = this._head;
            while (node != null)
            {
                if (node.Value.Equals(link))
                {
                    return node.Value;
                }
                node = node.Next;
            }

            return null;
        }

        public ILink GetLinkByUrl(string url)
        {
            var node = this._head;
            while (node != null)
            {
                if (node.Value.Url.Equals(url))
                {
                    return node.Value;
                }
                node = node.Next;
            }

            return null;
        }

        public ILink GetLastVisitedLink()
        {
            return this._head.Value;
        }

        public int RemoveLinks(string url)
        {
            var node = this._head;
            int count = 0;
            while (node != null)
            {
                if (node.Value.Url.Contains(url))
                {
                    count++;
                    if (node.Equals(this._head))
                    {
                        this._head = this._head.Next;
                    }
                    else if (node.Equals(this._tail))
                    {
                        this._tail.Previous.Next = null;
                        this._tail = this._tail.Previous;
                    }
                    else
                    {
                        node.Previous.Next = node.Next;
                        node.Next.Previous = node.Previous;
                    }
                }
                node = node.Next;
            }

            this._count -= count;

            return count;
        }

        public string ViewHistory()
        {
            var sb = new StringBuilder();
            var node = this._head;
            while (node != null)
            {
                sb.AppendLine($"-- {node.Value.Url} {node.Value.LoadingTime}s");
                node = node.Next;
            }
            return sb.ToString();
        }
    }
}
