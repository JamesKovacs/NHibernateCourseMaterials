using System;
using Iesi.Collections;
using NHibernate.Event;

namespace NHibernateDemo
{
    class SoftDeleteEventListener : IDeleteEventListener
    {
        public void OnDelete(DeleteEvent @event)
        {
            Console.WriteLine("Deleting");
        }

        public void OnDelete(DeleteEvent @event, ISet transientEntities)
        {
            Console.WriteLine("Deleting multiple");
        }
    }
}