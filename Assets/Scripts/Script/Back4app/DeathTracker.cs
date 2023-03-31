using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class DeathTracker : MonoBehaviour
{
    public TMP_Text enemyDeathText;
    private const string m_RegexRequester = "\"Count\":(\\d+)";
    private string m_UserId;
    private int currentDeath;
    private const string m_RegexId = "\"objectId\":\"(\\w+)";
    private string emailInput = "Stevengagnon01@hotmail.com";
    private string paswdInput = "Poulet_123!";
    [SerializeField] private bool m_IsIdentityConfirm;


    void Start()
    {
        // test();
        // StartCoroutine(CreateUser());
        StartCoroutine(LoginUser());
        // Debug.Log(countRequest);
    }

    public IEnumerator CreateUser()
    {
        const string url = "https://parseapi.back4app.com/users";
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/users", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SetRequestHeader("Content-type", "application/json");

            var data = new { username = emailInput, email = emailInput, password = paswdInput };
            var json = JsonConvert.SerializeObject(data);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Email Already register to database");
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
        }
    }
    public IEnumerator LoginUser()
    {
        string url = "https://parseapi.back4app.com/login";
        string mainUrl = $"{url}?username={emailInput}&password={paswdInput}";
        
        using (var request =  UnityWebRequest.Get(mainUrl))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("cannot login");
                yield break;
            }
            m_IsIdentityConfirm = true;
            Debug.Log(request.downloadHandler.text);
        }
    }
   

//need to be an enum an non pure to get value orr void async
    public async void test()
    {
        var x = (await taGetStringDataBaseValue());
        // test y  = new test(x);
    }

    public IEnumerator CreateDeathTracker()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/DeathTracker", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");

            var json = "{\"Name\": \"Enemy\",\"Count\": 0}";

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
        }
    }

    public IEnumerator GetDeathTracker()
    {
        string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"objectId\":\"Enemy\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
            var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
            // var matchesId = Regex.Matches(request.downloadHandler.text, "\"objectId\":\"(\\w+)", RegexOptions.Multiline);
            var countRequest = matches.First().Groups[1].Value;

            //currentDeath = Convert.ToInt32(countRequest);
            enemyDeathText.text = countRequest;
        }
    }

//async function

//get string value
    private async Task<string> taGetStringDataBaseValue()
    {
        const string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"Name\":\"Enemy\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SendWebRequest();

            while (!request.isDone)
            {
                print("aaaaaaaaaa");
                await Task.Delay(1);
            }

            if (request.result != UnityWebRequest.Result.Success) return null;

            var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
            var countRequest = matches.First().Groups[1].Value;
            return countRequest;
        }
    }

    //get int value
    private async Task<int> GetIntDataBaseValue(string url, Regex regex)
    {
        const string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"Name\":\"Enemy\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SendWebRequest();


            while (!request.isDone) await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success) return -1;


            var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);

            return Convert.ToInt32(matches.First().Groups[1].Value);
        }
    }

    //get id value
    private async Task<bool> GetUserDataBaseId()
    {
        string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"Name\":\"Enemy\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            while (!request.isDone) await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success) return false;

            Debug.Log(request.downloadHandler.text);
            var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
            var countRequest = matches.First().Groups[1].Value;
            currentDeath = Convert.ToInt32(countRequest);
            m_UserId = Regex.Matches(request.downloadHandler.text, "\"objectId\":\"(\\w+)", RegexOptions.Multiline)
                .First().Groups[1].Value;

            // enemyDeathText.text = countRequest;
            return true;
        }
    }


    public IEnumerator GetDeathTrackerAndSetUserID()
    {
        string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"Name\":\"Enemy\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
            var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
            var countRequest = matches.First().Groups[1].Value;
            currentDeath = Convert.ToInt32(countRequest);
            m_UserId = Regex.Matches(request.downloadHandler.text, "\"objectId\":\"(\\w+)", RegexOptions.Multiline)
                .First().Groups[1].Value;

            enemyDeathText.text = countRequest;
        }
    }

    public IEnumerator UpdateDeathTrackerValue()
    {
        string uri = $"https://parseapi.back4app.com/classes/DeathTracker/{m_UserId}";
        var json = $"{{\"Count\": {currentDeath + 1}}}";

        using (var request = UnityWebRequest.Put(uri, json))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            enemyDeathText.text =
                Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline).First()
                    .Groups[1].Value;
            Debug.Log(request.downloadHandler.text);
        }
    }
}