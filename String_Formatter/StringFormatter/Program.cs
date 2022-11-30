

using System.Linq.Expressions;
using String_Formatter;

 void main()
{
    var user = new User("Петя", "Иванов");
    var fullName = user.GetGreeting();   
}
 main();
public class User
{
    public string FirstName { get; }
    public string LastName { get; }
    
    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public object GetValue(object obj, string propertyName)
    {
        var value = obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        return value;
    }

    private static Func<object, object> CreateExpression(Type type, string propertyName)
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
    public string GetGreeting()
    {

     //  var a=  CreateExpression(typeof(User), "FirstName");
     // var aa= a(new User("jfjf", "sf"));
     // var aa1= a(new User("e65246", "sf"));
      return StringFormatter.Shared.Format(
            "{{{FirstName}}} {}транслируется {test} в {FirstName}", this); 
    }
}

