using System;

[Serializable]
public class User
{
    public string username;
    public string password;

    public User() {
    }

    public User(string name, string pass)
    {
        username = name;
        password = pass;
    }
}