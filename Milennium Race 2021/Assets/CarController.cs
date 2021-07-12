using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //public GameObject flw, frw, blw, brw;
    private Rigidbody2D rb;
    public float maxAcceleration = 4;
    public float accelerationTime = 4;
    public float maxSteering = 35;
    public float steeringTime = 35;
    public float frictionCefficient = 1f;
    // Start is called before the first frame update
    public GameObject[] wheels;
    public GameObject[] driveWheels;
    public GameObject[] steeringWheels;

    private float carAcceleration = 0f;
    //private float accelerationVelocity = 0.0f;
    private float carSteering = 0f;
    private float steeringVelocity = 0.0f;

    public float nitro = 10.0f;

    private bool drifting = false;
    private bool braking = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector2(0, +0.2f);
    }

    void AddForceAtPosition(Vector3 force, Vector3 pos)
    {
        Debug.DrawLine(pos, pos + force, Color.blue);
        rb.AddForceAtPosition(force, pos);
    }

    void Update()
    {

        //DEBUG
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(0, 0, 0);
        }

        var v = Input.GetAxis("Vertical");
        carAcceleration = v * maxAcceleration ;

        if (Input.GetKey(KeyCode.LeftShift) && nitro > 0f)
        {
            carAcceleration *= 1.5f;
            nitro -= Time.deltaTime;
        }

        var h = -Input.GetAxis("Horizontal");
        carSteering = Mathf.SmoothDamp(carSteering, h * maxSteering, ref steeringVelocity, steeringTime);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.centerOfMass = new Vector2(0, -0.1f);
            drifting = true;
            braking = true;
        }
        else if (Mathf.Abs(Vector3.Angle(rb.velocity, transform.up)) < 10)
        {
            rb.centerOfMass = new Vector2(0, +0.2f);
            braking = false;
            drifting = false;
        }
        else 
        {
            braking = false; 
        }

        if (Mathf.Abs(Vector3.Angle(rb.velocity, transform.up)) > 10 || drifting)
        {
            foreach (var wheel in driveWheels)
            {
                wheel.GetComponent<TrailRenderer>().emitting = true;
            }
        }
        else
        {
            foreach (var wheel in driveWheels)
            {
                wheel.GetComponent<TrailRenderer>().emitting = false;
            }
        }

    }

    void addFriction(GameObject wheel, Vector3 dir, float coefficient = 1.0f)
    {
        var velocity = rb.GetPointVelocity(wheel.transform.position);
        var friction = -frictionCefficient * rb.mass * Vector2.Dot(velocity, dir) * dir.normalized * coefficient;
        AddForceAtPosition(friction, wheel.transform.position);
    }

    void FixedUpdate()
    {
        foreach (var wheel in steeringWheels)
        {
            wheel.transform.localEulerAngles = new Vector3(0f, 0f, carSteering);
        }

        foreach (var wheel in driveWheels)
        {
            if (!braking)
            {
                AddForceAtPosition(wheel.transform.up * carAcceleration, wheel.transform.position);
            }
            else
            {
                addFriction(wheel, wheel.transform.up, 0.05f);
            }
            
        }

        foreach (var wheel in wheels)
        {
            addFriction(wheel, wheel.transform.right);
        }
    }
}
