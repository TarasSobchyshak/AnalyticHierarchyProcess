using System;
using System.Linq.Expressions;

namespace AHP.App.DependencyPropertyExtensions
{
    public static class ExpressionExtensions
    {
        public static string RetrieveMemberName<TArg, TRes>(this Expression<Func<TArg, TRes>> propertyExpression)
        {
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                UnaryExpression unaryExpression = propertyExpression.Body as UnaryExpression;
                if (unaryExpression != null)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }
            if (memberExpression != null)
            {
                ParameterExpression parameterExpression = memberExpression.Expression as ParameterExpression;
                if (parameterExpression != null && parameterExpression.Name == propertyExpression.Parameters[0].Name)
                    return memberExpression.Member.Name;
            }
            throw new ArgumentException("Invalid expression.", "propertyExpression");
        }
    }
}
