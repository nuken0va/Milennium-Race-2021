using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public float acceleration;
    public float steering;
    public float frictionCefficient = 0.1f;
    //private float wheelBase;
    public GameObject frontWheel;
    public GameObject backWheel;
    private Rigidbody2D rb;
    // private Rigidbody2D fwrb, bwrb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //wheelBase = transform.localScale.y;
        //steering = steering * Mathf.Deg2Rad;
        //fwrb = frontWheel.GetComponent<Rigidbody2D>();
        //bwrb = backWheel.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            /*
            var fwVelocity = rb.GetPointVelocity(frontWheel.transform.position);
            var bwVelocity = rb.GetPointVelocity(backWheel.transform.position);
            var fwFriction = - frictionCefficient * (Vector3)fwVelocity;
            var bwFriction = - frictionCefficient * (Vector3)bwVelocity;

            Debug.DrawLine(frontWheel.transform.position,
                           frontWheel.transform.position + fwFriction,
                           Color.blue);
            Debug.DrawLine(backWheel.transform.position,
               backWheel.transform.position + bwFriction,
               Color.blue);

            rb.AddForceAtPosition(fwFriction, frontWheel.transform.position);
            rb.AddForceAtPosition(bwFriction, backWheel.transform.position);
            */
            var fwVelocity = rb.GetPointVelocity(frontWheel.transform.position);
            var bwVelocity = rb.GetPointVelocity(backWheel.transform.position);
            var fwFriction1 = -frictionCefficient * Vector2.Dot(fwVelocity, frontWheel.transform.up) * frontWheel.transform.up.normalized;
            var bwFriction1 = -frictionCefficient * Vector2.Dot(bwVelocity, backWheel.transform.up) * backWheel.transform.up.normalized;
            var fwFriction2 = -frictionCefficient * 0.05f * Vector2.Dot(fwVelocity, frontWheel.transform.right) * frontWheel.transform.right.normalized;
            var bwFriction2 = -frictionCefficient * 0.05f * Vector2.Dot(bwVelocity, backWheel.transform.right) * backWheel.transform.right.normalized;
            Debug.DrawLine(frontWheel.transform.position,
                           frontWheel.transform.position + fwFriction1,
                           Color.blue);
            Debug.DrawLine(backWheel.transform.position,
                           backWheel.transform.position + bwFriction1,
                           Color.blue);
            Debug.DrawLine(frontWheel.transform.position,
                           frontWheel.transform.position + fwFriction2,
                           Color.blue);
            Debug.DrawLine(backWheel.transform.position,
                           backWheel.transform.position + bwFriction2,
                           Color.blue);

            rb.AddForceAtPosition(fwFriction1, frontWheel.transform.position);
            rb.AddForceAtPosition(bwFriction1, backWheel.transform.position);
            rb.AddForceAtPosition(fwFriction2, frontWheel.transform.position);
            rb.AddForceAtPosition(bwFriction2, backWheel.transform.position);
        }
        else
        {
            var carSteering = -Input.GetAxis("Horizontal") * steering;
            var carAcceleration = Input.GetAxis("Vertical") * acceleration;
            frontWheel.transform.localEulerAngles = new Vector3(0f, 0f, carSteering);

            /*
            var FwAngle = frontWheel.transform.localEulerAngles.z * Mathf.Deg2Rad;
            var BwAngle = backWheel.transform.localEulerAngles.z * Mathf.Deg2Rad;
            Vector3 FwHeading = new Vector3(Mathf.Cos(FwAngle), Mathf.Sin(FwAngle), 0);
            Vector3 BwHeading = new Vector3(Mathf.Cos(BwAngle), Mathf.Sin(BwAngle), 0);
            //DEBUG
            {

                Debug.DrawLine( (Vector3)(frontWheel.transform.position + 0.1f * FwHeading), 
                                (Vector3)(frontWheel.transform.position - 0.1f * FwHeading), 
                                Color.red);
                Debug.DrawLine( (Vector3)(backWheel.transform.position + 0.1f * BwHeading),
                                (Vector3)(backWheel.transform.position - 0.1f * BwHeading),
                                Color.red);
            }*/
            //DEBUG

            Debug.DrawLine(frontWheel.transform.position - 0.1f * frontWheel.transform.right,
                           frontWheel.transform.position + 0.1f * frontWheel.transform.right,
                           Color.red);
            Debug.DrawLine(backWheel.transform.position - 0.1f * backWheel.transform.right,
                           backWheel.transform.position + 0.1f * backWheel.transform.right,
                           Color.red);

            //rb.AddForceAtPosition(frontWheel.transform.right * carAcceleration, frontWheel.transform.position);
            rb.AddForceAtPosition(backWheel.transform.right * carAcceleration, backWheel.transform.position);

            Debug.DrawLine(transform.position, transform.position + (Vector3)rb.velocity, Color.green);

            var fwVelocity = rb.GetPointVelocity(frontWheel.transform.position);
            var bwVelocity = rb.GetPointVelocity(backWheel.transform.position);
            var fwFriction = - frictionCefficient * Vector2.Dot(fwVelocity, frontWheel.transform.up) * frontWheel.transform.up.normalized;
            var bwFriction = - frictionCefficient * Vector2.Dot(bwVelocity, backWheel.transform.up) * backWheel.transform.up.normalized;

            Debug.DrawLine(frontWheel.transform.position,
               frontWheel.transform.position + fwFriction,
               Color.blue);
            Debug.DrawLine(backWheel.transform.position,
               backWheel.transform.position + bwFriction,
               Color.blue);

            rb.AddForceAtPosition(fwFriction, frontWheel.transform.position);
            rb.AddForceAtPosition(bwFriction, backWheel.transform.position);
        }

    }

    /*
     * Неплохая попытка
     *  var carSteering = - Input.GetAxis("Horizontal") * steering;
        var carAcceleration = Input.GetAxis("Vertical") * acceleration;
        var carLocation = new Vector2(transform.position.x, transform.position.y);
        var carHeading = transform.eulerAngles.z * Mathf.Deg2Rad;
        var dt = Time.fixedDeltaTime;
        //make another gameObject?
        Vector2 frontWheelPosition = carLocation + wheelBase / 2 * new Vector2(Mathf.Cos(carHeading), Mathf.Sin(carHeading));
        Vector2 backWheelPosition = carLocation - wheelBase / 2 * new Vector2(Mathf.Cos(carHeading), Mathf.Sin(carHeading));
        Vector2 frontWheelHeading = new Vector2(Mathf.Cos(carHeading + carSteering), Mathf.Sin(carHeading + carSteering));
        Vector2 backWheelHeading = new Vector2(Mathf.Cos(carHeading), Mathf.Sin(carHeading));
        Debug.DrawLine((Vector3)(frontWheelPosition + 0.1f * frontWheelHeading), (Vector3)(frontWheelPosition - 0.1f*frontWheelHeading), Color.red);
        Debug.DrawLine((Vector3)(backWheelPosition + 0.1f * backWheelHeading), (Vector3)(backWheelPosition - 0.1f * backWheelHeading), Color.red);
       
        rb.AddForceAtPosition(frontWheelHeading * carAcceleration, frontWheelPosition);
        rb.AddForceAtPosition(backWheelHeading * carAcceleration, backWheelPosition);

        float k = 5f;
        Vector2 frontWheelPerp = Vector2.Perpendicular(frontWheelHeading).normalized;
        Vector2 frontWheelFrictrion = Vector2.Dot(rb.velocity, frontWheelPerp) * (-frontWheelPerp) * k;

        Vector2 backWheelPerp = Vector2.Perpendicular(backWheelHeading).normalized * k;
        Vector2 backWheelFrictrion = Vector2.Dot(rb.velocity, backWheelPerp) * (-backWheelPerp);

        Debug.DrawLine((Vector3)(frontWheelPosition + frontWheelFrictrion), (Vector3)(frontWheelPosition), Color.blue);
        Debug.DrawLine((Vector3)(backWheelPosition + backWheelFrictrion), (Vector3)(backWheelPosition), Color.blue);
        rb.AddForceAtPosition(frontWheelFrictrion, frontWheelPosition);
        rb.AddForceAtPosition(backWheelFrictrion, backWheelPosition);*/
    /* Из Интернета
     * 
     * float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 speed = transform.up * (v * acceleration);
        rb.AddForce(speed);

        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / 5.0f);
            //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
        }
        else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / 5.0f);
            //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
        }

        Vector2 forward = new Vector2(0.0f, 0.5f);
        float steeringRightAngle;
        if (rb.angularVelocity > 0)
        {
            steeringRightAngle = -90;
        }
        else
        {
            steeringRightAngle = 90;
        }

        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


        Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

        rb.AddForce(rb.GetRelativeVector(relativeForce));*/
    /*
     * ПЕРВЫНЕ ПОПЫТКИ
    private Rigidbody2D m_Rigidbody2D;
    private Transform m_Transform;

    public float speed = 1f;
    public float angleSpeed = 1f;
    public float rotAxisDist = 1f;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = -Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;
        var zAngle = m_Transform.eulerAngles.z * Mathf.Deg2Rad;
        var force = new Vector2(Mathf.Cos(zAngle) * verticalMove, Mathf.Sin(zAngle) * verticalMove);
        print(force);
        m_Rigidbody2D.AddForce(force*10);
        /*
        var zAngle = m_Transform.eulerAngles.z * Mathf.Deg2Rad;

        float horizontalMove = - Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;
        if (horizontalMove != 0f)
        { 
            var rotAxis = m_Transform.position + new Vector3(- Mathf.Sin(zAngle) * rotAxisDist, Mathf.Cos(zAngle) * rotAxisDist, 0);
            //print(horizontalMove);
            var deg = verticalMove * 360 / (2 * Mathf.PI * rotAxisDist);
            m_Transform.RotateAround(rotAxis, Vector3.back, deg);
            print(deg);

            zAngle = m_Transform.eulerAngles.z * Mathf.Deg2Rad;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(Mathf.Cos(zAngle) * verticalMove, Mathf.Sin(zAngle) * verticalMove);
        }
        */
    //}

}
