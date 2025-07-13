using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float maxTorque = 1500f;
    public float maxSteerAngle = 15f;

    // These will be set in the Inspector
    public string verticalInput = "Vertical";
    public string horizontalInput = "Horizontal";

    void Update()
    {
        float acceleration = Input.GetAxis(verticalInput) * maxTorque;
        float steering = Input.GetAxis(horizontalInput) * maxSteerAngle;

        ApplyAcceleration(acceleration);
        ApplySteering(steering);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void ApplyAcceleration(float torque)
    {
        frontLeftWheel.motorTorque = torque;
        frontRightWheel.motorTorque = torque;
        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;
    }

    void ApplySteering(float angle)
    {
        frontLeftWheel.steerAngle = angle;
        frontRightWheel.steerAngle = angle;
    }
}
