using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FlightMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private StarterAssetsInputs _input;
    [SerializeField]
    [Range(0.01f, 5.0f)] [Tooltip("Up/Down turn rate")]
    private float pitchMultiplier = 1.0f;
    [SerializeField]
    [Range(0.01f, 5.0f)] [Tooltip("Left/Right turn rate")]
    private float yawMultiplier = 1.0f;

    [SerializeField]
    private float flightSpeed = 100.0f;

    private void OnEnable()
    {
        rb = false ? GetComponent<Rigidbody>() : rb;
        _input = false ? GetComponent<StarterAssetsInputs>() : _input;

        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    void Start()
    {
        rb.maxAngularVelocity = 2.5f;
        rb.drag = 6;
        rb.angularDrag = 8;

    }

    void FixedUpdate()
    {
        Yaw();
        Pitch();
        Roll(); //important that roll comes last of the 3
        Velocity();
    }

    //for vertical control
    void Pitch()
    {
        float pitch = _input.steering.y * pitchMultiplier;
        rb.AddTorque(transform.right * pitch);

        //rotate witch for juicyness

    }

    void Yaw()
    {
        float yaw = _input.steering.x * yawMultiplier;
        rb.AddTorque(transform.up * yaw);

        //rotate witch for juicyness

    }

    //currently just locks roll in the Z axis, because we are not making a flight sim here
    void Roll()
    {
        Quaternion current = transform.rotation;
        Quaternion zLockedRot = new Quaternion();
        zLockedRot.eulerAngles = new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            0);
        
        transform.rotation = Quaternion.Slerp(current, zLockedRot, 4 * Time.fixedDeltaTime);
    }

    void Velocity()
    {
        //accellerate and deccellerate
        if(_input.accelerate)
        {
            rb.AddRelativeForce(new Vector3(0.0f, 0.0f, flightSpeed));
            rb.drag = 5;
            rb.angularDrag = 12;
        }
        else if(_input.decelerate)
        {
            rb.drag = 6;
            rb.angularDrag = 8;
        }
        else
        {
            rb.drag = 2;
            rb.angularDrag = 8;
        }
    }
}
