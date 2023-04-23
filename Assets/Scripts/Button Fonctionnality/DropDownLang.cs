using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropDownLang : DropDownController<PossibleLanguage>
{
    [SerializeField] private LanguageController m_LangController;

    public void SetCurrentUserLanguage()
    {
        m_DropDown.value = m_DropDown.options.FindIndex(value
            => value.text == m_LangController.GetLanguage());
    }
    public void ChangeUserLanguage(Enum x)
    {
        if (Enum.TryParse(base.GetDropDownValue(), out PossibleLanguage nextLang))
        {
            m_LangController.SetNewLanguage(nextLang);
        }
    }
}

public abstract class DropDownController<T> : MainBehaviour where T : struct, Enum
{
    private List<string> m_ListOfEnum;

    protected TMPro.TMP_Dropdown m_DropDown;

    [SerializeField] private UnityEvent m_OnInitFirstValue;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_DropDown = GetComponent<TMPro.TMP_Dropdown>();
    }


    protected override void OnStart()
    {
        base.OnStart();
        InitDroDown();
        m_OnInitFirstValue.Invoke();
    }

    private void InitDroDown()
    {
        if (m_DropDown && Init(out m_ListOfEnum))
        {
            m_DropDown.AddOptions(m_ListOfEnum);
            return;
        }

        print("Enum List Not initialise");
    }


    private bool Init(out List<string> listOfEnum)
    {
        listOfEnum = new List<string>();
        foreach (var data in Enum.GetNames(typeof(T)))
        {
            if (Enum.TryParse(data, out T enumValue))
            {
                listOfEnum.Add(data);
            }
        }

        return listOfEnum.Count > 0;
    }

    public string GetDropDownValue()
    {
        return m_DropDown.options[m_DropDown.value].text;
    }
}