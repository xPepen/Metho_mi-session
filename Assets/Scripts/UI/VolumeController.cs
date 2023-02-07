using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider m_volumeSlider;
    [SerializeField] private float defaultValue = 0.35f;

    private void Awake()
    {
        if (!m_volumeSlider)
        {
            m_volumeSlider = GetComponent<Slider>();
        }
    }
    void Start()
    {
        print(!PlayerPrefs.HasKey("m_volumeParam"));
        if (!PlayerPrefs.HasKey("m_volumeParam"))
        {
            PlayerPrefs.SetFloat("m_volumeParam", defaultValue);
        }
        LoadVolume();
        SaveVolume();
    }
    private void LoadVolume()
    {
        m_volumeSlider.value = PlayerPrefs.GetFloat("m_volumeParam");
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("m_volumeParam", m_volumeSlider.value);
    }
    public void ChangeVolume(float value)
    {
        AudioListener.volume = Mathf.Log10(value) * 30;
        SaveVolume();
    }
}
