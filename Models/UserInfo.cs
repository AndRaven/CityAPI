

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class UserInfo
{
    public int UserId {get; set;}

    public string FirstName {get; set;}

    public string LastName {get; set;}

    public string Username {get; set;}

    public string City {get; set;}

    public UserInfo(int userId, string firstName, string lastName, string userName, string city) 
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Username = userName;
        City = city;
    }
}