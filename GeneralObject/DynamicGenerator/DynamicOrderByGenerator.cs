using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GeneralObject.DynamicGenerator
{
    public static class DynamicOrderByGenerator
    {
        public static Func<T, T> OrderByGenerator<T>(string Field = "")
        {
            //string[] EntityFields;

            // Get Properties of the T
            //if (Fields == "")
            //EntityFields = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToArray();
            //else
            var EntityFields = Field.Split(','); 

            // Input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");

            // Create a property expression for the property to order by
            var property = Expression.Property(xParameter, Field);

            var orderByExpression = Expression.Lambda<Func<T, T>>(property, xParameter);

            return orderByExpression.Compile();
            // Expression "o => new T { Field1 = o.Field1}"
            // Compile to Func<T,T> 

        }
    }
}
