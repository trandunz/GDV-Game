using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ProgressionBar : MonoBehaviour
{
    private bool Change;
    private float TargetValue;

    public float Value;
    public float MaxValue;
    [SerializeField] public Gradient Gradient;
    [SerializeField] private Image Bar;
    [SerializeField] private Slider Slider;
    [SerializeField] private float Interpolation;


    void Start()
    {
        Slider = transform.GetChild(0).GetComponent<Slider>();
    }

    void Update()
    {
        //Check whether TargetValue has changed so that the SetHealth() function is activated
        TargetValue = Value / MaxValue;

        if (Slider.value != TargetValue) { Change = true; }
        if (Change) { SetHealth(); }
    }

    public void SetHealth()
    {
        Slider.value = Mathf.Lerp(Slider.value, TargetValue, Interpolation * Time.deltaTime);
        Bar.color = Gradient.Evaluate(Slider.normalizedValue);
        
        if (Mathf.Round(Slider.value * 10000) == Mathf.Round(TargetValue * 10000))
        {
            Slider.value = TargetValue; Change = false;
        }
    }
}
