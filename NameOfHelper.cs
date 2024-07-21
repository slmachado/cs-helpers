using System.Linq.Expressions;

namespace Helpers;

public static class NameOfHelper
{
    public static String Nameof<T, TProp>(this T obj, Expression<Func<T, TProp>> propertyAccessor)
    {
        if (propertyAccessor.Body is not MemberExpression body)
            throw new ArgumentException("'expression' should be a member expression");
        return body.Member.Name;
    }
}