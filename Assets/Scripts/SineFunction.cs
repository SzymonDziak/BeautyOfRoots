using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineFunction : MonoBehaviour
{
    Vector3 origionalPosition;

    // Update is called once per frame
    void Update()
    {
        if (Fractal.Instance.enableSineFunction)
        {
            transform.position = new Vector3(transform.position.x, function(transform.position.x, Time.time), transform.position.z);
        }
    }
    public static float function(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }
}
