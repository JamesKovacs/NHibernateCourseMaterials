using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using NHibernateDemo.Domain;

namespace NHibernateDemo
{
    public class MoneyType : IUserType
    {
        public bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            // Store Money in two columns
//            var type = (string)NHibernateUtil.String.NullSafeGet(rs, names[0]);
//            var amount = (decimal)NHibernateUtil.Decimal.NullSafeGet(rs, names[1]);
            // Store Money in one string column as "CA$42.42"
            var s = NHibernateUtil.String.NullSafeGet(rs, names) as string;
            if (s == null) return null;

            var type = s.Substring(0, 2);
            var amount = decimal.Parse(s.Substring(3));
            return new Money(amount, type);
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            object valueToSet;
            if(value == null)
            {
                valueToSet = DBNull.Value;
            }
            else
            {
                var money = (Money) value;
                valueToSet = string.Format("{0}${1}", money.CurrencyType, money.Value);
            }
            NHibernateUtil.String.NullSafeSet(cmd, valueToSet, index);
//            object amountToSet;
//            object typeToSet;
//            if(value == null)
//            {
//                amountToSet = DBNull.Value;
//                typeToSet = DBNull.Value;
//            }
//            else
//            {
//                var money = (Money) value;
//                amountToSet = money.Value;
//                typeToSet = money.CurrencyType;
//            }
//            NHibernateUtil.String.NullSafeSet(cmd, typeToSet, 0);
//            NHibernateUtil.Decimal.NullSafeSet(cmd, amountToSet, 1);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
//            get { return new[] {SqlTypeFactory.GetString(20), SqlTypeFactory.Decimal}; }
            get { return new[] {SqlTypeFactory.GetString(20)}; }
        }

        public Type ReturnedType
        {
            get { return typeof (Money); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}