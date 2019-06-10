using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MovementAxis
{ Horizontal, Vertical }

public class SyncSliderValues : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GunTopMovement movement;
    [SerializeField] MovementAxis movementAxis;
    [SerializeField] Text text;

    // Use this for initialization
    void Start()
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        if (this.movementAxis == MovementAxis.Horizontal)
            slider.value = movement.horizontalTurnSpeed;
        else if (this.movementAxis == MovementAxis.Vertical)
            slider.value = movement.verticalTurnSpeed;

        text.text = slider.value.ToString();
    }

    public void SyncValues()
    {
        if (this.movementAxis == MovementAxis.Horizontal)
            movement.horizontalTurnSpeed = slider.value;
        else if (this.movementAxis == MovementAxis.Vertical)
            movement.verticalTurnSpeed = slider.value;
        else
            Debug.Log("Something went wrong");

        text.text = slider.value.ToString();
    }
}
