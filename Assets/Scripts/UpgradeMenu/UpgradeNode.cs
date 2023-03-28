public class UpgradeNode
{
    public string UpgradeDefinition { get; private set; }
    public float UpgradeValue { get; private set; }
    public System.Action UpgradeAction { get; private set; }
    public bool UseFloatValue { get; private set; }
    public bool UseStringValue { get; private set; }
    public bool UseActionOnly { get; private set; }
    public UpgradeNode(System.Action _upgradeAction, float value)
    {
        this.UpgradeAction = _upgradeAction;
        this.UpgradeValue = value;
        this.UpgradeDefinition = null;
        UseFloatValue = true;
    }
    public UpgradeNode(System.Action _upgradeAction, string _definition)
    {
        this.UpgradeAction = _upgradeAction;
        this.UpgradeDefinition = _definition;
        UseStringValue = true;
    }
    public UpgradeNode(System.Action _upgradeAction)
    {
        this.UpgradeAction = _upgradeAction;
        UseActionOnly = true;
    }
}