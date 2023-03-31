using System.Collections.Generic;
using UnityEngine;


public class SpellManager : MainBehaviour
{
    [SerializeField] private List<SpellNode> ListOfSpell;
    private Dictionary<SpellEnum, GameObject> m_DictionaryOfSpell;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_DictionaryOfSpell = new();
        CreateSpells();
    }

    private void CreateSpells()
    {
        foreach (var _spell in ListOfSpell)
        {
            var _copy = Instantiate(_spell.SpellObject, Vector3.zero, Quaternion.identity);
            m_DictionaryOfSpell.Add(_spell.spellType, _copy);
        }
    }

    public GameObject GetSpell(SpellEnum _name) => m_DictionaryOfSpell?[_name];
    public IUpgradebleSpell GetSpellInterface(SpellEnum _name) => m_DictionaryOfSpell?[_name].GetComponent<IUpgradebleSpell>();
}