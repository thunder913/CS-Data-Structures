namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Data : IRepository
    {
        public Data()
        {
            
        }

        private Dictionary<int, List<IEntity>> parents = new Dictionary<int,List<IEntity>>();

        private Dictionary<int, IEntity> entities = new Dictionary<int, IEntity>();

        private MaxHeap<IEntity> maxHeap { get; set; } = new MaxHeap<IEntity>();

        public int Size => this.maxHeap.Size;

        public void Add(IEntity entity)
        {
            this.maxHeap.Add(entity);
            entities.Add(entity.Id, entity);
            if (!parents.ContainsKey((int)entity.ParentId))
            {
                parents.Add((int)entity.ParentId, new List<IEntity>());
            }

            parents[(int)entity.ParentId].Add(entity);


        }

        public IRepository Copy()
        {
            return this;
        }

        public IEntity DequeueMostRecent()
        {
            var element = this.maxHeap.RemoveTopElement();
            entities.Remove(element.Id);
            return element;
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.maxHeap.GetAsList());
        }

        public List<IEntity> GetAllByType(string type)
        {
            return this.maxHeap.GetByType(type);
        }

        public IEntity GetById(int id)
        {
            if (!this.entities.ContainsKey(id))
            {
                return null;
            }
            return this.entities[id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            if (!parents.ContainsKey(parentId))
            {
                return new List<IEntity>();
            }
            return this.parents[parentId];
        }

        public IEntity PeekMostRecent()
        {
            return this.maxHeap.PeekMostRecent();
        }
    }
}
