using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeble
{
    public void InitUpgrade();
    public void UpgradeStat(int _value);
    public string UpgradeName();
}
