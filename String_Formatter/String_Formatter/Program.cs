

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

    public string GetGreeting()
    {
        Expression<Func<User, string>> firstNameAccessor = (User user) => user.FirstName;
        Expression<Func<User, string>> lastNameAccessor = (User user) => user.LastName;
        
        
      return StringFormatter.Shared.Format(
            "{{{FirstName}}} {}транслируется {test} в {FirstName}", this); 
    }
}

