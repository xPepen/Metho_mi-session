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
        return TranslatorManager.GetCurrentLanguage();
    } 
    public void SetNewLanguage(PossibleLanguage langValue)
    {
         TranslatorManager.SetCurrentLanguage(langValue);
    }
    

   
}
