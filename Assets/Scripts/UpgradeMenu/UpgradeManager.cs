using System;
using System.Collections.Generic;
using UnityEngine;



public class UpgradeManager : MainBehaviour
{
   [SerializeField] private Player m_PlayerRef;

   [SerializeField] private List<Spell> spell;
   
   [SerializeField] private Dictionary<GameObject, GameObject> Spell;

   protected override void OnStart()
   {
      base.OnStart();
      spell = m_PlayerRef.ListOfSpell;
   }

   public void OnUpgradeGunDamage()
   {
      
   }
   public void OnUpgradeSpellDamage(int _index)
   {
      
   }
   public void OnUpgradeRange(int _index)
   {
      
   }

   public void OnUpgradeAttackCount(int _index)
   {
      
   }

   public void OnUpgradeHP()
   {
      
   }

   public void OnUpgradeSpeed()
   {
      
   }
}
