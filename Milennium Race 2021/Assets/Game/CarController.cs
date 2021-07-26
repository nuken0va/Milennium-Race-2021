using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource aus;

    private Bar nitroBar;

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

    float currentVolume = 0f;

    private bool isDrifting = false;
    private bool isBraking = false;
    private bool isNitro = false;

    private bool lightTrail = false;
    private bool wheelTrail = false;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aus = GetComponent<AudioSource>();
        rb.centerOfMass = new Vector2(0, normalCenterOfMass);
        curNitro = maxNitro;
        nitroBar = GameObject.Find("NitroBar").GetComponent<Bar>();
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
            aus.pitch = 1.2f;
        }
        else 
        {
            isNitro = false;
            aus.pitch = 1f;
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

        if (v == 0)
        {
            aus.volume = Mathf.MoveTowards(aus.volume, 0, 0.03f);
        }
        if (v != 0)
        {
            aus.volume = Mathf.MoveTowards(aus.volume, 1, 0.03f);
        }

        UpdateTrails();
    }

    void addFriction(GameObject wheel, Vector3 dir)
    {
        var velocity = rb.GetPointVelocity(wheel.transform.position);
        //var friction = - rb.mass * frictionCefficient * Mathf.Sign(Vector2.Dot(velocity, dir)) * dir.normalized;
        var friction = - rb.mass * frictionCefficient * Vector2.Dot(velocity, dir) * dir.normalized;
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
                addFriction(wheel, wheel.transform.up);
            }
            
        }

        foreach (var wheel in wheels)
        {
            addFriction(wheel, wheel.transform.right);
        }
    }
}
