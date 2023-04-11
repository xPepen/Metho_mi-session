using UnityEngine;
[CreateAssetMenu]
public class ScritablePromoCode : ScriptableObject
{
    public string UserId;
    public string UserToken;
    public bool ReviveCode;
    public bool SpeedCode;
    public bool SkinCode;

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
}
