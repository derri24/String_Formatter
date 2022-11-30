using System.Linq.Expressions;

namespace String_Formatter;

public static class ExpressionCreator
{
    public static Func<object, object> Create(Type type, string propertyName)
    {
        var parameterExpression = Expression.Parameter(typeof(object), "instance");
        Expression<Func<object, object>> expression;
        try
        {
            Expression  body = Expression.PropertyOrField(Expression.TypeAs(parameterExpression, type), propertyName);
            expression = Expression.Lambda<Func<object, object>>(body, parameterExpression);
        }
        catch
        {
            return null;
        }
        return expression.Compile();
    }
}