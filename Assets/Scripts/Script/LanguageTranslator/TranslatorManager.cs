using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PossibleLanguage
{
    English,
    French,
    Spanish
}


public class TranslatorManager : MainBehaviour
{
    private string CurrentLanguage;
    private const string m_UserLang = "UserLanguage";
    [SerializeField] private PossibleLanguage m_DefaultLang = PossibleLanguage.English;
    private List<ITranslateBehaviour> m_textEntities = new();

    protected override void OnAwake()
    {
        base.OnAwake();
        InitLang();
    }

    private string GetPlayerPrefLang() => PlayerPrefs.GetString(m_UserLang);
    private void SetPlayerPrefLang(ReadOnlySpan<char> newlang) => PlayerPrefs.SetString(m_UserLang, newlang.ToString());


    private bool IsLangEmpty()
    {
        var userLang = GetPlayerPrefLang();
        return string.IsNullOrEmpty(userLang) || string.IsNullOrWhiteSpace(userLang);
    }

    private void InitLang()
    {
        var defaultLang = m_DefaultLang.ToString();
        if (IsLangEmpty() || !(defaultLang.Equals(GetPlayerPrefLang())))
        {
            CurrentLanguage = defaultLang;
            SetPlayerPrefLang(CurrentLanguage);
            return;
        }

        CurrentLanguage = GetPlayerPrefLang();
    }

    private void UpdateLanguages()
    {
        m_textEntities.ForEach(instance =>instance.UpdateText());
    }

    public string GetCurrentLanguage() => CurrentLanguage;

    public void SetCurrentLanguage(PossibleLanguage lang)
    {
        var newLang = lang.ToString();
        if (CurrentLanguage == newLang) return;

        if (!Enum.IsDefined(typeof(PossibleLanguage), lang))
            throw new InvalidEnumArgumentException(nameof(lang), (int)lang, typeof(PossibleLanguage));

        CurrentLanguage = newLang;
        SetPlayerPrefLang(newLang);
        UpdateLanguages();
    }

    public void Subscribe(ITranslateBehaviour instance)
    {
        m_textEntities.Add(instance);
    }
}