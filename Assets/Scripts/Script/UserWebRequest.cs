using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public enum EPromoCode
{
    REVIVE,
    SKIN,
    SPEED,
}

public class UserWebRequest : MainBehaviour
{
    //maybe static value for the class + request
    [SerializeField] private TMP_InputField m_PromoCodeInput;
    [SerializeField] private TextMeshProUGUI m_StateMessage;

    private const string m_WebClassRequest = "https://parseapi.back4app.com/classes/";
    private const string m_PromoCodeClass = "PromoCode";
    [SerializeField] private ScritablePromoCode m_PlayerPromoCode;
//

    public void TEST_ME()
    {
        Test();
    }

    private async void Test()
    {
        BinaryReaderWriter.Serialize(0, nameof(Player));
        var path = Application.persistentDataPath + nameof(Player) + ".dat";
        var @byteData  = File.ReadAllBytes(path);
        await UploadeFile(byteData);
    }
    
    private async Task<string> UploadeFile(byte[] data)
    {
        string uri = $"https://parseapi.back4app.com/users/{m_PlayerPromoCode.UserId}";

        using (var request = UnityWebRequest.Put(uri,data))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-type", "application/octet-stream");


            // string filepath = Path.Combine() 
            await request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                // var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                //     .Value;
                // return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
                Debug.LogError("Dont work");
                return "ERROR";
            }

            print(request.downloadHandler.text);
            return request.downloadHandler.text;
        }
    }

//
    public void OnTryPromoCodee()
    {
        AsyncUserTask(m_PromoCodeInput.text);
        //AsyncUserTask("abc");
    }

    private string GenerateRandomWordCode(int wordLenght, int customNum)
    {
        const int asciiStart = 97; // ASCII code for 'a'
        const int asciiEnd = 122; // ASCII code for 'z'

        var random = new System.Random();
        StringBuilder codeBuilder = new StringBuilder(wordLenght);

        for (int i = 0; i < wordLenght; i++)
        {
            if (i == wordLenght / 2)
            {
                int num = random.Next(0, 999999);
                codeBuilder.Append(num);
            }
            else if (i == wordLenght / 3)
            {
                codeBuilder.Append(customNum);
            }
            else
            {
                var character = (char)random.Next(asciiStart, asciiEnd + 1);
                codeBuilder.Append(character);
            }
        }

        return codeBuilder.ToString();
    }

    private void ClearInput()
    {
        m_PromoCodeInput.text = string.Empty;
    }

    private void SetMessage(string message)
    {
        m_StateMessage.text = message;
    }

    private void ClearMessage()
    {
        m_StateMessage.text = string.Empty;
    }

    private bool CanUseCode(EPromoCode code)
    {
        return code switch
        {
            EPromoCode.SKIN => !m_PlayerPromoCode.SkinCode,
            EPromoCode.SPEED => !m_PlayerPromoCode.SpeedCode,
            EPromoCode.REVIVE => !m_PlayerPromoCode.ReviveCode,
            _ => false
        };
    }

    private async void AsyncUserTask(string input)
    {
        string id = await GetClassID(m_PromoCodeRequest(m_PromoCodeClass, input), "\"HasBeenUse\":\"(\\w+)");
        if (string.IsNullOrWhiteSpace(id))
        {
            SetMessage("the promo code isnt a valid one");
            ClearInput();
            return;
        }


        var codeReq = await GetCodeStateAndType(m_PromoCodeRequest(m_PromoCodeClass, input));
        var isUsed = codeReq[0];
        var codeType = codeReq[1];

        if (isUsed == "true")
        {
            SetMessage("This code is already used!");
        }

        else if (Enum.TryParse(codeType, out EPromoCode code) && CanUseCode(code))
        {
            await UpdateClassData(id, "{\"HasBeenUse\": true}");
            m_PlayerPromoCode.UpdatePromoCode(code);
            await UpdateUserData();
            SetMessage($"Code type {code} added");
        }
        else
        {
            SetMessage("Your already have this type of code");
        }
        ClearInput();
    }
    
    private async Task<string> UpdateUserData()
    {
        string uri = $"https://parseapi.back4app.com/users/{m_PlayerPromoCode.UserId}";
        string json = m_PlayerPromoCode.CreateJsonFile();
    
        using (var request = UnityWebRequest.Put(uri, json))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Session-Token", m_PlayerPromoCode.UserToken);
            request.SetRequestHeader("Content-type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
            
                var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0]
                    .Value;
                Debug.LogError(Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode)));

                return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
            }
            
            print(request.downloadHandler.text);
            return string.Empty;
        }
    }


    private string m_PromoCodeRequest(string classDataName, string code)
        => $"{m_WebClassRequest}{classDataName}?where={{\"CodeValue\":\"{code}\"}}";

    //private string GetFinalPath(string classLocation) => m_WebClassRequest + classLocation;


    private async Task<string> GetClassID(string reqWeb, string reqBool)
    {
        using (var request = UnityWebRequest.Get(reqWeb))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);

            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error + "not found");
                return "null";
            }

            var result = Regex.Match(request.downloadHandler.text, "\"objectId\":\"(\\w+)").Groups[1].Value;

            if (!String.IsNullOrEmpty(result) &&
                Regex.Match(request.downloadHandler.text, reqBool).Groups[1].Value != "true;")
            {
                return result;
            }

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// return [0] is true or false
    /// return [1] is the code type enum into string
    /// <returns></returns>
    private async Task<string[]> GetCodeStateAndType(string reqWeb)
    {
        const string isUsedCode = "\"HasBeenUse\":(\\w+)";
        const string codeType = "\"CodeType\":\"(\\w+)";
        // const string isUsedCode = "\"HasBeenUse\":\"(false \"|\"true)";
        //const string isUsedCode = "\"HasBeenUse\":\"(false|true)";

        using (var request = UnityWebRequest.Get(reqWeb))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error + "not found");
                return null;
            }

            string getBoolType = Regex.Match(request.downloadHandler.text, isUsedCode).Groups[1].Value;
            if (getBoolType != "true")
            {
                var reqResult = Regex.Match(request.downloadHandler.text, codeType).Groups[1].Value;
                if (!string.IsNullOrEmpty(reqResult))
                {
                    return new[] { getBoolType, reqResult };
                }
            }

            return new[] { getBoolType, null };
        }
    }

    private async Task<string> UpdateClassData(string classId, string valueToUpdate)
    {
        string uri = $"https://parseapi.back4app.com/classes/PromoCode/{classId}";

        using (var request = UnityWebRequest.Put(uri, valueToUpdate))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-type", "application/json");

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(valueToUpdate));
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                // var ErrorCode = Regex.Match(request.downloadHandler.text, @"(\d+)", RegexOptions.Multiline).Groups[0].Value;
                // return Back4AppError.GetErrorMessage(Convert.ToInt32(ErrorCode));
                Debug.LogError(request.error);
            }
        }
        return string.Empty;
    }
    

    public async Task CreateNewPromoCode(string reqWeb, string codeValue, string codeType)
    {
        string uri = $"https://parseapi.back4app.com/classes/{m_PromoCodeClass}";
        string json = JsonConvert.SerializeObject(new
        {
            CodeValue = codeValue,
            CodeType = "REVIVE",
        });
        using (var request = UnityWebRequest.Post(uri, json))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");

            // var json = "{\"CodeValue\": \"Enemy\",\"Count\": 0}";
            //var x = $"{{\"CodeValue\":\"{codeValue}\",\"CodeType\":\"{codeType}}}";
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }

            print(request.downloadHandler.text);

            Debug.Log("new class object created");
        }
    }
}