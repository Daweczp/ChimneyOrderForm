namespace ChimneyOrderForm.Options;

public class UserCredentials
{
    public string Password { get; set; }

    public string Username { get; set; }
}

public class LoginCredentials
{
    public List<UserCredentials> Users { get; set; }
}