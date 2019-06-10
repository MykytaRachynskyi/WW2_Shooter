using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRateSlider : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    [SerializeField] GunTopShooter shooter;

    // Use this for initialization
    void Start()
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        slider.value = shooter.shotsPerSecond;
    }

    public void SyncValues()
    {
        shooter.shotsPerSecond = slider.value;
        text.text = slider.value.ToString();
    }
}
