namespace Wolah.IM.Model.LoginModel;

public class LoginControlModel
{
    private string _userName;
    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }
    private string _userPassword;
    public string UserPassword
    {
        get => _userPassword;
        set => _userPassword = value;
    }
}