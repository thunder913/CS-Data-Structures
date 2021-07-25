namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons { get; set; } = new List<IWeapon>();

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons = new List<IWeapon>();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.GetById(weapon.Id) != null;

        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Category == category)
                {
                    this.weapons[i].Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            var currentWeapon = this.GetById(weapon.Id);
            if (currentWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (currentWeapon.Ammunition < ammunition)
            {
                return false;
            }

            currentWeapon.Ammunition -= ammunition;
            return true;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == id)
                {
                    return this.weapons[i];
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.weapons.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            var currentWeapon = this.GetById(weapon.Id);
            if (currentWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }


            if (weapon.Ammunition + ammunition > weapon.MaxCapacity)
            {
                weapon.Ammunition = weapon.MaxCapacity;
            }
            else
            {
                weapon.Ammunition += ammunition;
            }

            return weapon.Ammunition;

        }

        public IWeapon RemoveById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == id)
                {
                    var removed = this.weapons[i];
                    this.weapons.RemoveAt(i);
                    return removed;
                }
            }

            throw new InvalidOperationException("Weapon does not exist in inventory!");
        }

        public int RemoveHeavy()
        {
            var removed = 0;
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Category == Category.Heavy)
                {
                    removed++;
                    this.weapons.RemoveAt(i);
                    i--;
                }
            }

            return removed;
        }

        public List<IWeapon> RetrieveAll()
        {
            return this.weapons;
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var toReturn = new List<IWeapon>();

            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Category >= lower && this.weapons[i].Category <= upper)
                {
                    toReturn.Add(this.weapons[i]);
                }
            }

            return toReturn;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            var firstIndex = -1;
            var secondIndex = -1;
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == firstWeapon.Id)
                {
                    firstIndex = i;
                }
                else if (this.weapons[i].Id == secondWeapon.Id)
                {
                    secondIndex = i;
                }
            }

            if (firstIndex == -1 || secondIndex == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (this.weapons[firstIndex].Category == this.weapons[secondIndex].Category)
            {
                var temp = this.weapons[firstIndex];
                this.weapons[firstIndex] = this.weapons[secondIndex];
                this.weapons[secondIndex] = temp;
            }

        }
    }
}
