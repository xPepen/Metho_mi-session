using UnityEngine;

public interface IUpgradeblePlayerStats
{
    public void OnStatHealthUpgrade(float _amount);
    public void OnStatSpeedUpgrade(float _amount);
    public void OnAddNewSpell(GameObject _entity);
}