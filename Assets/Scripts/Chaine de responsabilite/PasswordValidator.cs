using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class PasswordValidator
{
    private SortedList<int, IUserValidateInput> m_LinkedList;
    private IUserValidateInput m_CurrentCheck;

    public PasswordValidator()
    {
        m_LinkedList = new SortedList<int, IUserValidateInput>();
        m_LinkedList.Add(0, new PasswordEmpty());
        m_LinkedList.Add(1, new PasswordEmptySpace());
        m_LinkedList.Add(2, new PasswordLength());
        m_LinkedList.Add(3, new PasswordFormat());
        m_LinkedList.Add(4, new PasswordRequestFail());
    }

    private void SetInterfaceData(IUserValidateInput current) => m_CurrentCheck = current;
    public string GetErrorMessage() => m_CurrentCheck.ErrorMessage();
    public string GetValidationMessage() => m_CurrentCheck.ValidateMessage();

    public bool CheckforRequest(string value)
    {
        for (int i = 0; i < m_LinkedList.Count; i++)
        {
            if (!m_LinkedList[i].ValidateInput(value))
            {
                SetInterfaceData(m_LinkedList[i]);
                return false;
            }
        }

        SetInterfaceData(m_LinkedList[m_LinkedList.Count - 1]);
        return true;
    }


    private class PasswordEmptySpace : IUserValidateInput
    {
        public bool ValidateInput(string input)
        {
            foreach (var c in input)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        public string ErrorMessage()
        {
            return "Password Cant conatain white space";
        }

        public string ValidateMessage()
        {
            return "Password is validate";
        }
    }

    private class PasswordEmpty : IUserValidateInput
    {
        public bool ValidateInput(string input)
        {
            return !string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input);
        }

        public string ErrorMessage()
        {
            return "Password doesnt contain character";
        }

        public string ValidateMessage()
        {
            return "Password is validate";
        }
    }

    private class PasswordLength : IUserValidateInput
    {
        public bool ValidateInput(string input)
        {
            return input.Length >= 8;
        }

        public string ErrorMessage()
        {
            return "Password Length minimun of 8";
        }

        public string ValidateMessage()
        {
            return "Password is validate";
        }
    }

    private class PasswordFormat : IUserValidateInput
    {
        public bool ValidateInput(string input)
        {
            return input.Where(char.IsLower).Any() && input.Where(char.IsUpper).Any();
        }

        public string ErrorMessage()
        {
            return "Password need 1 upper case and 1 lower case";
        }

        public string ValidateMessage()
        {
            return "Password is validate";
        }
    }

    private class PasswordRequestFail : IUserValidateInput
    {
        public bool ValidateInput(string input)
        {
            return !string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input);
        }

        public string ErrorMessage()
        {
            return "Password Insnt valid";
        }

        public string ValidateMessage()
        {
            return "Password is validate";
        }
    }
}