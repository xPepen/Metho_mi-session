using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    private Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
         m_Material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        m_Material.SetFloat("_ProgressBar",(Mathf.Sin(Time.time + 1f ) / 2f ));
    }
}
