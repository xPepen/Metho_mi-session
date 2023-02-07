using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePig : Enemy
{
    protected override void OnAttack()
    {
       
            if (!CanAttack)
            {
                return;
            }

        //m_playerRef.OnHit();

    }

    
}
