using UnityEngine;

public class LanguageController : MonoBehaviour
{

    [SerializeField]private TranslatorManager @TranslatorManager;
    void Start()
    {
        TranslatorManager = FindObjectOfType<TranslatorManager>();
    }

    public string GetLanguage()
    {
        Debug.LogError("Current lang = "  + TranslatorManager.GetCurrentLanguage() );
        return TranslatorManager.GetCurrentLanguage();
    } 
    public void SetNewLanguage(PossibleLanguage langValue)
    {
        Debug.LogError("WE SET NEW LANG");
         TranslatorManager.SetCurrentLanguage(langValue);
    }
    

   
}
