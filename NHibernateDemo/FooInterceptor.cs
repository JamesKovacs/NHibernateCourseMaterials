using System.Collections.Generic;
using NHibernate;

namespace NHibernateDemo
{
    public class FooInterceptor : EmptyInterceptor
    {
    }

    public class ChainedInterceptor : EmptyInterceptor
    {
        readonly IEnumerable<IInterceptor> iterceptors;

        public ChainedInterceptor(params IInterceptor[] iterceptors)
        {
            this.iterceptors = iterceptors;
        }

        // Need to implement each one and chain the interceptors collection
        // and handle any exceptions properly
    }
}