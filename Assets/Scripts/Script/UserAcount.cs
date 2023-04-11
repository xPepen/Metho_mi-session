using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[Flags]
public enum AccountState
{
    NONE = 1,
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
    [SerializeField] private UnityEvent m_OnRequestStart;
    [SerializeField] private UnityEvent m_OnRequestEnd;

    [SerializeField] private ScritablePromoCode m_PlayerPromoCode;
    private Dictionary<string, string> m_UserData;

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

        m_OnRequestEnd.Invoke();
    }

    private async void AsyncUserTask(Task<string> task, UnityEvent _event = null)
    {
        //await GetClassID();
        m_OnRequestStart.Invoke();
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
        if (_event != null && m_IsIdentityConfirm)
        {
            _event.Invoke();
        }
    }


    public void UpdateUserDataServer()
    {
        LaunchTask(UpdateClassData());
    }

    public void CheckForPromoCode()
    {
        const int possibility = 3;
        int count = 0;
        foreach (var data in m_UserData)
        {
            if (data.Value == "true")
            {
                m_PlayerPromoCode.UpdatePromoCode(data.Key);
                count++;
                if (count == possibility) break;
            }
        }

        return;
        
    }


    public void SetMessageState(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            m_StateMessage.text = message;
        }
    }

    private string CreateJsonFile()
    {
        Dictionary<string, bool> json = new();
        if (m_UserData[nameof(m_PlayerPromoCode.SkinCode)] == "false" && m_PlayerPromoCode.SkinCode)
        {
            json.Add("SkinCode", true);
        }

        if (m_UserData[nameof(m_PlayerPromoCode.ReviveCode)] == "false" && m_PlayerPromoCode.ReviveCode)
        {
            json.Add("SpeedCode", true);
        }

        if (m_UserData[nameof(m_PlayerPromoCode.SpeedCode)] == "false" && m_PlayerPromoCode.SpeedCode)
        {
            json.Add("ReviveCode", true);
        }

        if (json.Count == 0)
        {
            return string.Empty;
        }

        return JsonConvert.SerializeObject(json);
    }

    private async void LaunchTask(Task<string> task)
    {
        SetMessageState(await task);
    }

    private async Task SetUserInformation(string userData)
    {
        m_UserData = new();
        JObject jObject = JObject.Parse(userData);
        await Task.Run(() =>
        {
            m_UserData.Add("SkinCode", jObject["SkinCode"].ToString().ToLower());
            m_UserData.Add("SpeedCode", jObject["SpeedCode"].ToString().ToLower());
            m_UserData.Add("ReviveCode", jObject["ReviveCode"].ToString().ToLower());
            m_UserData.Add("objectId", jObject["objectId"].ToString().ToLower());
            m_UserData.Add("sessionToken", jObject["sessionToken"].ToString().ToLower());
        });
        m_PlayerPromoCode.UserId = m_UserData["objectId"];
        m_PlayerPromoCode.UserToken = m_UserData["sessionToken"];
        print("data has been added" + Time.time);
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

            if (request.result != UnityWebRequest.Result.Success)
            {
                var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                    .Value;
                return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
            }


            return "Account Created, Need to confirm you email!";
        }
    }

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
                var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                    .Value;
                return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
            }

            m_UserData = new();

            await SetUserInformation(request.downloadHandler.text);
            m_IsIdentityConfirm = true;
            SetMessageState("LOgin confirm!!!");
            return string.Empty;
        }
    }

    private async Task<string> UpdateClassData(string classId = "")
    {
        string uri = $"https://parseapi.back4app.com/users/{m_UserData["objectId"]}";
        string json = CreateJsonFile();
        if (json == string.Empty)
        {
            return "Noting to update";
        }

        using (var request = UnityWebRequest.Put(uri, json))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Session-Token", m_UserData["sessionToken"]);
            request.SetRequestHeader("Content-type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                    .Value;
                return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
            }

            print(request.downloadHandler.text);
            return string.Empty;
        }
    }


    private async Task<string> UploadImage(string filename)
    {
        string uri = $"https://parseapi.back4app.com/files/{filename}";

        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "image/png");

            // string filepath = Path.Combine() 
            await request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                    .Value;
                return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
            }

            print(request.downloadHandler.text);
            m_IsIdentityConfirm = true;
            return request.downloadHandler.text;
        }
    }
}