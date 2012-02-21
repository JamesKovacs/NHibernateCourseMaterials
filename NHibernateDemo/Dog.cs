using System;

namespace NHibernateDemo
{
    public class Dog : Mammal
    {
    }

    public class Cat : Mammal
    {
    }

    public class Mammal : Animal
    {
    }

    public class Lizard : Reptile
    {
        
    }

    public class Reptile : Animal
    {
    }

    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}