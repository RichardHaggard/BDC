using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Utils
{
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = 
            typeof(string).GetMethod("Contains");
        
        private static readonly MethodInfo StartsWithMethod =
            typeof(string).GetMethod("StartsWith", new Type [] {typeof(string)});

        private static readonly MethodInfo EndsWithMethod =
            typeof(string).GetMethod("EndsWith", new Type [] { typeof(string)});

        [CanBeNull]
        public static Expression<Func<T, bool>> GetExpression<T>([NotNull] IList<IFilterItem> filters)
        {
            if  (filters.Count == 0)
                return null;

            var param = Expression.Parameter(typeof (T), "t" );
            var exp = filters.Select(filter => 
                GetExpression<T>(param, filter))
                .Aggregate<Expression, Expression>(null, (current, tmp) =>
                    (current == null) ? tmp : Expression.AndAlso(current, tmp));

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>([NotNull] ParameterExpression param, [NotNull] IFilterItem filter)
        {
            if (param  == null) throw new ArgumentNullException(nameof(param));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var member   = Expression.Property(param, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case EnumFilterOps.Equals:
                    return Expression.Equal(member, constant);

                case EnumFilterOps.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case EnumFilterOps.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case EnumFilterOps.LessThan:
                    return Expression.LessThan(member, constant);

                case EnumFilterOps.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case EnumFilterOps.Contains:
                    return Expression.Call(member, ContainsMethod, constant);

                case EnumFilterOps.StartsWith:
                    return Expression.Call(member, StartsWithMethod, constant);

                case EnumFilterOps.EndsWith:
                    return Expression.Call(member, EndsWithMethod, constant);

                default:
                    return null ;
            }
        }

        private static BinaryExpression GetExpression<T>(
            ParameterExpression param, 
            IFilterItem filter1, 
            IFilterItem filter2)
        {
            var bin1 = GetExpression<T>(param, filter1);
            var bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
}
