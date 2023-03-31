using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class EmailValidator
    {
        private SortedList<float, IUserValidateInput> m_LinkedList;
        private IUserValidateInput m_CurrentCheck;

        public EmailValidator()
        {
            m_LinkedList = new SortedList<float, IUserValidateInput>();
            m_LinkedList.Add(0, new EmailEmpty());
            m_LinkedList.Add(1, new EmailRegex());
            m_LinkedList.Add(2, new EmailRequestFail());
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
                    Debug.Log("index : " + i + $" = {m_LinkedList[i]}");
                    return false;
                }
            }

            SetInterfaceData(m_LinkedList[m_LinkedList.Count - 1]);
            return true;
        }




        private class EmailRegex : IUserValidateInput
        {
            public bool ValidateInput(string input)
            {
                const string m_EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                return Regex.IsMatch(input, m_EmailRegex);
            }

            public string ErrorMessage()
            {
                return "Email User format isnt valid";
            }

            public string ValidateMessage()
            {
                return "Email is validate";
            }
        }

        private class EmailEmpty : IUserValidateInput
        {
            public bool ValidateInput(string input)
            {
                return !string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input);
            }

            public string ErrorMessage()
            {
                return "Email doesnt contain character";
            }

            public string ValidateMessage()
            {
                return "Email is validate";
            }
        }

        private class EmailRequestFail : IUserValidateInput
        {
            public bool ValidateInput(string input)
            {
                return !string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input);
            }

            public string ErrorMessage()
            {
                return "Email already exist";
            }

            public string ValidateMessage()
            {
                return "Email confirm";
            }
        }
    }

