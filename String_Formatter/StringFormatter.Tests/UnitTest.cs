using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using String_Formatter;

namespace Tests;

[TestClass]
public class UnitTest
{
    private class User
    {
        public string FirstName { get; }
        public string LastName { get; }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    private User user;

    public UnitTest()
    {
        user = new User("Петя", "Иванов");
    }

    [TestMethod]
    public void CheckInterpolationTest()
    {
        Assert.AreEqual("{Middle}", StringFormatter.Shared.Format("{Middle}", user));
        Assert.AreEqual("Петя Иванов", StringFormatter.Shared.Format("{FirstName} {LastName}", user));
        Assert.AreEqual("Петя Петя", StringFormatter.Shared.Format("{FirstName} {FirstName}", user));
        Assert.AreEqual("Петя Иванов {}", StringFormatter.Shared.Format("{FirstName} {LastName} {}", user));
        Assert.AreEqual("Петя Иванов {MiddleName}",
            StringFormatter.Shared.Format("{FirstName} {LastName} {MiddleName}", user));
    }


    [TestMethod]
    public void CheckScreeningTest()
    {
        Assert.ThrowsException<ArgumentException>(() =>
            StringFormatter.Shared.Format("{}}", user));
        Assert.ThrowsException<ArgumentException>(() =>
            StringFormatter.Shared.Format("{{{{{{{{FirstName}} транслируется в {FirstName}", user));
        Assert.AreEqual("{FirstName} транслируется в Петя",
            StringFormatter.Shared.Format("{{FirstName}} транслируется в {FirstName}", user));
        Assert.AreEqual("{{FirstName}} транслируется в Петя",
            StringFormatter.Shared.Format("{{{FirstName}}} транслируется в {FirstName}", user));
    }
}