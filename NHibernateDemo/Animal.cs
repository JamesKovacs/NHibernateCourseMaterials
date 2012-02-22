using System;

namespace NHibernateDemo
{
    public class Dog : Mammal
    {
        public virtual int BarksPerSecond { get; set; }
    }

    public class Cat : Mammal
    {
    }

    public abstract class Mammal : Animal
    {
        public virtual int HairsPerSquareCentimeter { get; set; }
    }

    public class Lizard : Reptile
    {
        
    }

    public abstract class Reptile : Animal
    {
    }

    public abstract class Animal
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, FullName: {1}", Id, Name);
        }
    }
}