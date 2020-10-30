using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MatBlazorDemo.Domain
{
    /// <summary>
    /// Enables the efficient, dynamic composition of query predicates.
    /// </summary>
    public static class PredicateBuilder
    {
        /*
        /// <summary>
        /// Creates a predicate that evaluates to true.
        /// </summary>
        public static Expression<Func<T, bool>> True<T>()
        {
            return param => true;
        }
        /// <summary>
        /// Creates a predicate that evaluates to false.
        /// </summary>
        public static Expression<Func<T, bool>> False<T>()
        {
            return param => false;
        }
        /// <summary>
        /// Creates a predicate expression from the specified lambda expression.
        /// </summary>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate;
        }
        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        /// <summary>
        /// Negates the predicate.
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }
        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        */

        /// <summary>
        /// 机关函数应用 True 时：单个 AND 有效，多个 AND 有效；单个 OR 无效，多个 OR 无效；混应时写在 AND 后的 OR 有效  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// 机关函数应用 False 时：单个 AND 无效，多个 AND 无效；单个 OR 有效，多个 OR 有效；混应时写在 OR 后面的 AND 有效  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector,
            IEnumerable<TValue> values)
        {
            var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var startWithCollection = values.Select(value => (Expression)Expression.Call(valueSelector.Body, startsWithMethod!, Expression.Constant(value, typeof(TValue))));
            var body = startWithCollection.Aggregate((Expression.Or));
            var p = Expression.Parameter(typeof(TElement));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public static IQueryable<TElement> WhereOrLike<TElement, TValue>(this IQueryable<TElement> query,
            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            return query.Where(BuildContainsExpression<TElement, TValue>(valueSelector, values));
        }

        public static Expression<Func<TElement, bool>> BuildEqualsExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector,
            IEnumerable<TValue> values)
        {
            var equalsWithMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            var equalCollection = values.Select(value => (Expression)Expression.Call(valueSelector.Body, equalsWithMethod!, Expression.Constant(value, typeof(TValue))));
            var body = equalCollection.Aggregate((Expression.Or));
            var p = Expression.Parameter(typeof(TElement));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public static IQueryable<TElement> WhereOrIn<TElement, TValue>(this IQueryable<TElement> query,
            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            return query.Where(BuildEqualsExpression<TElement, TValue>(valueSelector, values));
        }
    }
}
