using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
[CreateAssetMenu]
public class ScritablePromoCode : ScriptableObject
{
    public string UserId;
    public string UserToken;
    public bool ReviveCode;
    public bool SpeedCode;
    public bool SkinCode;

    public Dictionary<string, string> AllUserData = new();
    public void UpdatePromoCode(EPromoCode code)
    {
        switch (code)
        {
            case EPromoCode.SKIN:
                SkinCode = true;
                break;
            case EPromoCode.SPEED:
                SpeedCode = true;
                break;
            default:
                ReviveCode = true;
                break;
        }
    }
    public void UpdatePromoCode(string code)
    {
        switch (code)
        {
            case "SkinCode":
                SkinCode = true;
                break;
            case "SpeedCode":
                SpeedCode = true;
                break;
            default:
                ReviveCode = true;
                break;
        }
    }
    
    public string CreateJsonFile()
    {
        Dictionary<string, bool> json = new();
        if (AllUserData[nameof(SkinCode)] == "false" && SkinCode)
        {
            json.Add("SkinCode", true);
        }

        if (AllUserData[nameof(ReviveCode)] == "false" && ReviveCode)
        {
            json.Add("SpeedCode", true);
        }

        if (AllUserData[nameof(SpeedCode)] == "false" && SpeedCode)
        {
            json.Add("ReviveCode", true);
        }

        if (json.Count == 0)
        {
            return string.Empty;
        }
        return JsonConvert.SerializeObject(json);
    }
}
