namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private Dictionary<int, IEntity> entities { get; set; } = new Dictionary<int, IEntity>();

        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity.Id, entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.FindById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                this.entities.Remove(id);
                return entity;
            }
            return null;
        }

        public IEntity Find(IEntity entity)
        {
            return this.FindById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities.Values);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.Values.GetEnumerator();
        }

        public void RemoveSold()
        {
            foreach (var item in this.entities)
            {
                if (this.entities[item.Key].Status == BaseEntityStatus.Sold)
                {
                    this.entities.Remove(this.entities[item.Key].Id);
                }
            }
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            try
            {
                if (!this.entities.Remove(oldEntity.Id))
                {
                    throw new Exception();
                }

                this.entities.Add(newEntity.Id ,newEntity);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Entity not found");
            }
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var toReturn = new List<IEntity>();

            foreach (var item in this.entities)
            {
                if (this.entities[item.Key].Status >= lowerBound && this.entities[item.Key].Status <= upperBound)
                {
                    toReturn.Add(this.entities[item.Key]);
                }
            }

            return toReturn;
        }

        public void Swap(IEntity first, IEntity second)
        {
            try
            {
                var temp = this.entities[first.Id];
                this.entities[first.Id] = this.entities[second.Id];
                this.entities[second.Id] = temp;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Entity not found");
            }
        }

        public IEntity[] ToArray()
        {
            var toReturn = new IEntity[this.entities.Count];
            int count = 0;
            foreach (var item in this.entities.Values)
            {
                toReturn[count++] = item;
            }
            return toReturn;
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            foreach (var key in this.entities.Keys)
            {
                if (this.entities[key].Status == oldStatus)
                {
                    this.entities[key].Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.entities.Values.GetEnumerator();
        }

        private IEntity FindById(int id)
        {
            return this.entities[id];
        }
    }
}
