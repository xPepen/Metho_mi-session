using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSwordHandler 
{
    [SerializeField] private int SwordCount;
    //private List<RotatingSword>
    /*public override void Attack(Vector2 _dir)
    {
    }*/
}


public class RotatingSword 
{
    /*protected override void OnUpdate()
    {
        base.OnUpdate();
        OnMove();
    }*/

    public void OnMove()
    {
        //transform.RotateAround(); = Quaternion.AngleAxis(Time.time * 100f , new Vector3(0,1,1)) * new Vector3(1,5,5);
        //transform.(Player.Instance.transform.position, new Vector3(0, 1, 1), 30 * Time.deltaTime);

    }
    /*public override void Attack(Vector2 _dir)
    {
        
    }*/
}
