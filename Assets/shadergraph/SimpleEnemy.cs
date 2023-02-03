using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private float m_hitvalue;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
      
            test();
       
    }

    private void test()
    {
        m_hitvalue = Mathf.Lerp(m_hitvalue, 0f, Time.deltaTime * 5f);
        print(m_hitvalue);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_hitvalue = 1f;
        }
        m_spriteRenderer.material.SetFloat("_HitValue", m_hitvalue);
    }
}
