using System.Collections.Generic;



//Implementation Design => Firebase
[System.Serializable]
public class User
{
    public string Uid;
    public string Username;
    public string Email;

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "uid", Uid },
            { "username", Username },
            { "email", Email }
        };
    }

    public static User FromDictionary(Dictionary<string, object> dict)
    {
        return new User
        {
            Uid = dict["uid"].ToString(),
            Username = dict["username"].ToString(),
            Email = dict["email"].ToString()
        };
    }
}
