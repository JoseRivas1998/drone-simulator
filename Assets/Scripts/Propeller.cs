using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{

    public bool clockwise;

    public float thrust;
    private float rpm;

    // Start is called before the first frame update
    void Start()
    {
        rpm = 0;
        thrust = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rpm = 200 * (thrust / 4f);
        float degreesPerSecond = rpm * (360f / 60f) * (clockwise ? 1f : -1f);
        transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime);
    }

}
