/// created by Matthew (mattlawr) from VGDC
/// for UI workshop (Jan 24th 2023)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // <-- allows us to use the slider component
using TMPro;

public class SliderControl : MonoBehaviour
{
    [Tooltip("Optional. Updated with the value of the slider")]
    public Text display;
    [Tooltip("Optional. Updated with the value of the slider")]
    public TMP_Text displayTMP;

    private Slider my_Slider;

    void Start()
    {
        // get the attached component
        my_Slider = GetComponent<Slider>();

        // add our own listener for the state of that component
        my_Slider.onValueChanged.AddListener(delegate
        {
            ValueChanged(my_Slider);
        });
    }

    protected virtual void ValueChanged(Slider change)
    {
        if (display) display.text = change.value.ToString();
        if (displayTMP) displayTMP.text = change.value.ToString();

    }

    public void UpdateVolume(float v)
    {
        // assumes input is a value from 0 to 100
        AudioListener.volume = Mathf.Clamp01(v / 100.0f);
    }
}
