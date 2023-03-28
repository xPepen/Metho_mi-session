using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public enum AccountState
{
    NONE = 0,
    CREATE = 1,
    LOGIN = 2
}


public class UserAcount : MainBehaviour
{
    [SerializeField] private TMP_InputField m_InputEmail;
    [SerializeField] private TMP_InputField m_InputPassword;
    [SerializeField] private TextMeshProUGUI m_StateMessage;
    [SerializeField] private bool m_IsIdentityConfirm;

    private EmailValidator m_EmailValidator;
    private PasswordValidator m_PasswordValidaor;
    private List<PasswordValidator> m_PasswordValidator;

    [SerializeField] private UnityEvent m_OnConnectionConfirm;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_EmailValidator = new EmailValidator();
        m_PasswordValidaor = new PasswordValidator();
    }

    public void UserAccountAction(string buttonJob)
    {
        if (buttonJob.ToUpper() == (nameof(AccountState.CREATE)))
        {
            AsyncUserTask((CreateUser(m_InputEmail.text, m_InputPassword.text)));
        }
        else if (buttonJob.ToUpper() == (nameof(AccountState.LOGIN)))
        {
            AsyncUserTask(LoginUser(m_InputEmail.text, m_InputPassword.text), m_OnConnectionConfirm);
        }
        else
        {
            Debug.LogError("Jobbutton isnt valide", this);
        }
    }


    private async void AsyncUserTask(Task<string> task, UnityEvent _event = null)
    {
        if (!m_EmailValidator.CheckforRequest(m_InputEmail.text))
        {
            SetMessageState(m_EmailValidator.GetErrorMessage());
            return;
        }

        if (!m_PasswordValidaor.CheckforRequest(m_InputPassword.text))
        {
            SetMessageState(m_PasswordValidaor.GetErrorMessage());
            return;
        }

        var message = await task;
        SetMessageState(message);
        if (_event != null)
        {
            _event.Invoke();
        }
    }


    private void SetMessageState(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            m_StateMessage.text = message;
        }
    }

    private async Task<string> CreateUser(string user, string password)
    {
        const string url = "https://parseapi.back4app.com/users";
        using (var request = new UnityWebRequest(url, "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SetRequestHeader("Content-type", "application/json");

            var data = new { username = user, email = user, password = password };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            await request.SendWebRequest();

            var result = request.result != UnityWebRequest.Result.Success
                ? "Email account already exist"
                : $"{request.downloadHandler.text} created";

            return result;
        }
    }

    private static readonly HttpClient HttpClient = new HttpClient();

    private async Task<string> LoginUser(string user, string password)
    {
        string url = "https://parseapi.back4app.com/login";
        string mainUrl = $"{url}?username={user}&password={password}";

        using (var request = UnityWebRequest.Get(mainUrl))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");

            await request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                return "Incorrect Email or password " + request.error;
            }

            m_IsIdentityConfirm = true;
            return "Login ";
        }
    }
}