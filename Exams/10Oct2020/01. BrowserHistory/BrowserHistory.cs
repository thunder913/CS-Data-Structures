namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private LinkCollection LinkCollection { get; set; } = new LinkCollection(null);

        public int Size => this.LinkCollection.Size;

        public void Clear()
        {
            this.LinkCollection.Clear();
        }

        public bool Contains(ILink link)
        {
            return this.LinkCollection.GetElement(link) != null;
        }

        // not optimal
        public ILink DeleteFirst()
        {
            this.AreLinksEmpty();
            return this.LinkCollection.DeleteLast();
        }

        public ILink DeleteLast()
        {
            AreLinksEmpty();

            return this.LinkCollection.DeleteFirst();
        }

        public ILink GetByUrl(string url)
        {
            return this.LinkCollection.GetLinkByUrl(url);
        }

        public ILink LastVisited()
        {
            AreLinksEmpty();

            return this.LinkCollection.GetLastVisitedLink();
        }

        public void Open(ILink link)
        {
            this.LinkCollection.Add(link);
        }

        public int RemoveLinks(string url)
        {
            AreLinksEmpty();
            return this.LinkCollection.RemoveLinks(url);

        }

        public ILink[] ToArray()
        {
            return this.LinkCollection.GetAllLinks().ToArray();
        }

        public List<ILink> ToList()
        {
            return this.LinkCollection.GetAllLinks().ToList();
        }

        public string ViewHistory()
        {
            if (this.Size == 0)
            {
                return "Browser history is empty!";
            }
            return this.LinkCollection.ViewHistory();
        }

        private void AreLinksEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
