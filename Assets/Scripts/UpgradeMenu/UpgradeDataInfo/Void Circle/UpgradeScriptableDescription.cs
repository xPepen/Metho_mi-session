using UnityEngine;

[CreateAssetMenu]
public class UpgradeScriptableDescription : ScriptableObject
{
    [SerializeField] private string m_ItemName;
    [TextArea] [SerializeField] private string m_UpgradeDefinition;
    public string UpgradeDefinition => m_UpgradeDefinition;
}