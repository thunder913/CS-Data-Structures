namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private List<IEnemy> enemies { get; set; } = new List<IEnemy>();

        private Dictionary<int, IEnemy> dictionaryEnemies = new Dictionary<int, IEnemy>();

        private MaxHeap<IEnemy> maxHeap = new MaxHeap<IEnemy>();

        private MinHeap<IEnemy> minHeap = new MinHeap<IEnemy>();

        public int Size => this.enemies.Count;

        // O(1)
        public bool Contains(IEnemy enemy)
        {
            if (this.dictionaryEnemies.ContainsKey(enemy.AttackSpeed))
            {
                return true;
            }

            return false;
        }

        // O(1)
        public void Create(IEnemy enemy)
        {
            if (!this.dictionaryEnemies.ContainsKey(enemy.AttackSpeed))
            {
                this.dictionaryEnemies.Add(enemy.AttackSpeed, enemy);
                this.enemies.Add(enemy);
                this.maxHeap.Add(enemy);
                this.minHeap.Add(enemy);
            }
        }

        // O(1)
        public IEnemy GetByAttackSpeed(int speed)
        {
            if (this.dictionaryEnemies.ContainsKey(speed))
            {
                return this.dictionaryEnemies[speed];
            }

            return null;
        }


        // O(n)
        public List<IEnemy> GetFaster(int speed)
        {
            var toReturn = new List<IEnemy>();

            for (int i = 0; i < this.Size; i++)
            {
                if (this.enemies[i].AttackSpeed > speed)
                {
                    toReturn.Add(this.enemies[i]);
                }
            }

            return toReturn;
        }

        // O(n)
        public IEnemy GetFastest()
        {
            return this.maxHeap.Peek();
        }

        // O(n)
        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.OrderByDescending(x => x.Health).ToArray();
        }

        // O(n)
        public List<IEnemy> GetSlower(int speed)
        {
            var toReturn = new List<IEnemy>();

            for (int i = 0; i < this.Size; i++)
            {
                if (this.enemies[i].AttackSpeed < speed)
                {
                    toReturn.Add(this.enemies[i]);
                }
            }

            return toReturn;
        }

        // O(n)
        public IEnemy GetSlowest()
        {
            this.CheckSize();
            return this.minHeap.Peek();
        }

        // O(n)
        public void ShootFastest()
        {
            this.CheckSize();

            var element = this.maxHeap.RemoveTopElement();
            
            this.dictionaryEnemies.Remove(element.AttackSpeed);
            this.enemies.Remove(element);
        }

        // O(n)
        public void ShootSlowest()
        {
            this.CheckSize();

            var element = this.minHeap.RemoveTopElement();

            this.dictionaryEnemies.Remove(element.AttackSpeed);
            this.enemies.Remove(element);
        }

        private void CheckSize()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
