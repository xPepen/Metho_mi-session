public interface IUpgradebleSpell
{
    public void OnRadiusUpgrade(float _value);
    public void OnDamageUpgrade(float _value);
    public void OnAttackRateUpgrade(float _value);
    public void OnTargetCountUpgrade(float _increment);
}