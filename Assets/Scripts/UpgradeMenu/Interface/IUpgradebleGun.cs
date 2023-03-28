using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradebleGun
{
    public void OnProjectileCountUpgrade(float _value);
    public void OnProjectileAngleUpgrade(float _value);
    public void OnFireRateUpgrade(float _value);
}
