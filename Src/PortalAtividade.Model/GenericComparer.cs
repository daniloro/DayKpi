using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PortalAtividade.Model
{
    public class GenericComparer<T> : IComparer<T>
    {
        private string _sortExpression;
        private string _sortDirection;

        public GenericComparer(string sortExpression, string sortDirection)
        {
            _sortExpression = sortExpression;
            _sortDirection = sortDirection;
        }

        public int Compare(T x, T y)
        {
            PropertyInfo propertyInfo = x.GetType().GetProperty(_sortExpression);
            IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
            IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);

            if (_sortDirection == "ASC")
            {
                return obj1.CompareTo(obj2);
            }
            else
            {
                return obj2.CompareTo(obj1);
            }
        }
    } 
}