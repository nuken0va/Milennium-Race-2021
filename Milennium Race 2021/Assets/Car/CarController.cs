using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public NitroBar nitroBar;

    [SerializeField] private float maxAcceleration = 4;
    [SerializeField] [Range(0, 55f)]  private float maxSteering = 35;
    [SerializeField] private float steeringTime = 35;
    [SerializeField] private float frictionCefficient = 1f;

    [SerializeField] [Range(-1.0f, 1f)] private float normalCenterOfMass = 0.2f;
    [SerializeField] [Range(-1.0f, 1f)] private float brakeCenterOfMass = -0.1f;


    public GameObject[] wheels;
    public GameObject[] driveWheels;
    public GameObject[] steeringWheels;
    public GameObject[] lights;

    private float carAcceleration = 0f;
    private float carSteering = 0f;
    private float steeringVelocity = 0.0f;

    float curNitro;
    public float maxNitro = 10.0f;

    private bool isDrifting = false;
    private bool isBraking = false;
    private bool isNitro = false;

    private bool lightTrail = false;
    private bool wheelTrail = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.centerOfMass = new Vector2(0, normalCenterOfMass);
        curNitro = maxNitro;
    }

    void AddForceAtPosition(Vector3 force, Vector3 pos)
    {
        Debug.DrawLine(pos, pos + force, Color.blue);
        rb.AddForceAtPosition(force, pos);
    }

    void UpdateAcceleration(float vertical)
    {
        if (vertical < 0)
        {
            vertical *= 0.5f;
        }
        carAcceleration = vertical * maxAcceleration * (isNitro ? 1.5f : 1f);
    }

    void UpdateSteering(float horizontal)
    {
        carSteering = Mathf.SmoothDamp(carSteering, horizontal * maxSteering, ref steeringVelocity, steeringTime);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && curNitro > 0f)
        {
            isNitro = true;
            curNitro -= Time.deltaTime;
            nitroBar.SetValue(curNitro / maxNitro);
        }
        else 
        {
            isNitro = false;
        }

        var v = Input.GetAxis("Vertical");
        UpdateAcceleration(v);

        var h = -Input.GetAxis("Horizontal");
        UpdateSteering(h);

        if (Input.GetKey(KeyCode.Space))
        {
            //sr.color = Color.green;
            isDrifting = true;
            isBraking = true;
            //rb.centerOfMass = new Vector2(0, brakeCenterOfMass);
        }
        else if (isDrifting && Mathf.Abs(Vector3.Angle(rb.velocity, transform.up)) < 10)
        {
            //sr.color = Color.red;
            isBraking = false;
            isDrifting = false;
            //rb.centerOfMass = new Vector2(0, normalCenterOfMass);
        }
        else
        {
            isBraking = false; 
        }

        UpdateTrails();
    }

    void addFriction(GameObject wheel, Vector3 dir, float coefficient = 1.0f)
    {
        var velocity = rb.GetPointVelocity(wheel.transform.position);
        var friction = - frictionCefficient * rb.mass * Vector2.Dot(velocity, dir) * dir.normalized * coefficient;
        AddForceAtPosition(friction, wheel.transform.position);
    }

    void UpdateTrails()
    {
        if (Mathf.Abs(Vector3.Angle(rb.velocity, transform.up)) > 10 || isDrifting)
        {
            if(!wheelTrail)
            {
                foreach (var wheel in driveWheels)
                {
                    wheel.GetComponent<TrailRenderer>().emitting = true;
                }
            }
            wheelTrail = true;
        }
        else
        {
            if (!wheelTrail)
            {
                foreach (var wheel in driveWheels)
                {
                    wheel.GetComponent<TrailRenderer>().emitting = false;
                }
            }
            wheelTrail = false;
        }
        if (isNitro && Vector2.Dot(rb.velocity, (Vector2)transform.up) > 0)
        {
            if (!lightTrail)
            {
                foreach (var light in lights)
                {
                    light.GetComponent<TrailRenderer>().emitting = true;
                }
            }
            lightTrail = true;
        }
        else
        {
            if (lightTrail)
            {
                foreach (var light in lights)
                {
                    light.GetComponent<TrailRenderer>().emitting = false;
                }
            }
            lightTrail = false;
        }
    }

    void FixedUpdate()
    {
        foreach (var wheel in steeringWheels)
        {
            wheel.transform.localEulerAngles = new Vector3(0f, 0f, carSteering);
        }

        foreach (var wheel in driveWheels)
        {
            if (!isBraking)
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
