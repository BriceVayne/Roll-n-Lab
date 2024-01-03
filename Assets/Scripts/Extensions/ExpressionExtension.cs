using System;
using System.Linq.Expressions;

namespace Extension
{
    public static class ExpressionExtension
    {
        public static string ExpressionToString<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("Expression is null");

            var lambdaExpressionBody = expression.Body;

            var memberExpression = lambdaExpressionBody as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = lambdaExpressionBody as UnaryExpression;
                if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;

                    if (memberExpression != null)
                        return memberExpression.Member.Name;
                }
            }
            else
            {
                //TODO : Test it or remove it.
                //gets something line "m.Field1.Field2.Field3", from here we just remove the prefix "m."
                string body = expression.Body.ToString();
                return body.Substring(body.IndexOf('.') + 1);
            }

            throw new ArgumentException("No property reference expression was found.", "expression");
        }
    }
}