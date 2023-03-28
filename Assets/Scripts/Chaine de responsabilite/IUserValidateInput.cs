using System.Text.RegularExpressions;

public interface IUserValidateInput
{
    bool ValidateInput(string input);
    string ErrorMessage();
    string ValidateMessage();
}


