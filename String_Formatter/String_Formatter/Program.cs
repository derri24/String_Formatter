

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
      return StringFormatter.Shared.Format(
            "{{FirstName}} транслируется в {FirstName}", this); 
    }
}

