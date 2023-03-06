public interface IUpgradebleProjectile
{
    public void OnDamageUpgrade(float _value);
    public void OnFireRateUpgrade(float _value);
    public void OnProjectileCountUpgrade(int _increment);
    public void OnProjectilePerAngleUpgrade(int _increment);
}