using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTopMovement : MonoBehaviour
{
    public float horizontalTurnSpeed;
    public float verticalTurnSpeed;
    public Transform carrot;

    [SerializeField] float maxVerticalAngle;
    [SerializeField] float maxHorizontalAngle;
    [SerializeField] bool windowsControls = false;
    Quaternion startRotation;
    Vector3 localUpAxis, localRightAxis, localForwardAxis;
    void Start()
    {
        startRotation = transform.localRotation;
        localUpAxis = transform.parent.transform.up;
        localForwardAxis = transform.parent.transform.forward;
        localRightAxis = transform.parent.transform.right;
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            windowsControls = true;
    }
    void Update()
    {
        if (windowsControls)
        {
            if (Input.GetKey(KeyCode.W))
                OnJoystickTurn(new Vector2(0, 1));
            if (Input.GetKey(KeyCode.A))
                OnJoystickTurn(new Vector2(-1, 0));
            if (Input.GetKey(KeyCode.S))
                OnJoystickTurn(new Vector2(0, -1));
            if (Input.GetKey(KeyCode.D))
                OnJoystickTurn(new Vector2(1, 0));
        }
    }

    public void OnJoystickTurn(Vector2 margin)
    {
        /*
        Vector3 temp = transform.localRotation.eulerAngles;
        temp.x += verticalTurnSpeed * margin.y * Time.deltaTime;
        if (temp.x > 180)
            temp.x -= 360f;
        temp.x = Mathf.Clamp(temp.x, -maxVerticalAngle, maxVerticalAngle);

        temp.z += horizontalTurnSpeed * margin.x * Time.deltaTime;
        if (temp.z > 180)
            temp.z -= 360f;
        temp.z = Mathf.Clamp(temp.z, -maxHorizontalAngle, maxHorizontalAngle);

        temp.y = 0f;*/

        //carrot.localEulerAngles = temp;
        float temp = carrot.localEulerAngles.y;
        if (temp > 180f)
            temp -= 360f;
        if ((temp > maxHorizontalAngle && margin.x < 0f) ||
            (temp < -maxHorizontalAngle && margin.x > 0f) ||
            (temp < maxHorizontalAngle && temp > -maxHorizontalAngle))
        {
            carrot.Rotate(Vector3.up, Time.deltaTime * margin.x * horizontalTurnSpeed);
        }


        temp = carrot.localEulerAngles.x;
        if (temp > 180f)
            temp -= 360f;
        if ((temp > maxVerticalAngle && margin.y < 0f) ||
            (temp < -maxVerticalAngle && margin.y > 0f) ||
            (temp < maxVerticalAngle && temp > -maxVerticalAngle))
        {
            carrot.Rotate(Vector3.right, Time.deltaTime * margin.y * verticalTurnSpeed);
        }
        /*
        if (carrot.localEulerAngles.x > 180f)
            temp = carrot.localEulerAngles.x - 360f;
        if (temp < 30f && temp > -30f)
            carrot.Rotate(Vector3.right, Time.deltaTime * margin.y * verticalTurnSpeed);
            */

        //Debug.Log("Horizontal: " + carrot.localEulerAngles.y);

        this.transform.LookAt(carrot.GetChild(0), Vector3.up);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 90f, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //Quaternion rot = Quaternion.Euler(temp);
        //transform.Rotate(transform.parent.right, temp.x, Space.Self);
        //transform.Rotate(transform.parent.up, temp.z, Space.Self);

        //transform.rotation = rot; // doesn't work
        //transform.localRotation = rot; // works
        //transform.localEulerAngles = temp; // same as previous

        //transform.RotateAroundLocal();

        //transform.parent.localEulerAngles = new Vector3(parentRotation, 0f, 0f); // should work but doesn't
    }
}