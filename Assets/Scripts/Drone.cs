using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public Propeller frontRightProp;
    public Propeller frontLeftProp;
    public Propeller backRightProp;
    public Propeller backLeftProp;
    public Rigidbody rb;

    public float thrustPerSecond;

    private const int FRONT_RIGHT = 0;
    private const int FRONT_LEFT = 1;
    private const int BACK_RIGHT = 2;
    private const int BACK_LEFT = 3;

    private Propeller[] props;
    private float[] propOffsets;
    private float[] targetOffsets;
    private bool hovering;
    private float totalBaseThrust;

    // Start is called before the first frame update
    void Start()
    {
        props = new Propeller[] { frontRightProp, frontLeftProp, backRightProp, backLeftProp };
        propOffsets = new float[props.Length];
        targetOffsets = new float[props.Length];
        totalBaseThrust = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);

    }

    private void HandleInput()
    {
        hovering = true;
        if (Input.GetKey(KeyCode.W))
        {
            hovering = false;
            IncreaseThrust(1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hovering = false;
            IncreaseThrust(-1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (hovering)
        {
            Hover();
        }
        ApplyThrust();
    }

    private void ApplyThrust()
    {
        for (int i = 0; i < props.Length; i++)
        {
            rb.AddForceAtPosition(transform.up * props[i].thrust, props[i].transform.position);
        }
    }

    private void Hover()
    {
        float velY = rb.velocity.y;
        if(velY > 0)
        {
            IncreaseThrust(-1);
        }
        else if(velY < 0)
        {
            IncreaseThrust(1);
        }
    }

    private void IncreaseThrust(float direction)
    {
        totalBaseThrust = Mathf.Max(0, totalBaseThrust + direction * thrustPerSecond * Time.deltaTime);
        UpdatePropsThrust();
    }

    private void UpdatePropsThrust()
    {
        for (int i = 0; i < props.Length; i++)
        {
            props[i].thrust = totalBaseThrust / 4f + propOffsets[i];
        }
    }

}
