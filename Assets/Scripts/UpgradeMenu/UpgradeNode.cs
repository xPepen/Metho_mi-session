public class UpgradeNode
{
    public string UgradeDefinition { get; private set; }
    public System.Action UpgradeAction { get; private set; }

    public UpgradeNode(System.Action _upgradeAction, string _definition)
    {
        this.UpgradeAction = _upgradeAction;
        this.UgradeDefinition = _definition;
    }
}