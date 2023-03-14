using UnityEngine;

[System.Serializable]
public class SpellNode
{
    [SerializeField] private string NodeName;
    [field: SerializeField] public SpellEnum spellType { get; private set; }
    [field: SerializeField] public GameObject SpellObject { get; private set; }
}