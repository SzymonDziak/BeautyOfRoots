using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [Header("Prefab")]
    public Transform cube;

    [Header("Resolution")]
    [Range(10, 100)]
    public int resolution;

    [Range(0, 1)]
    public int functions;

    Transform[] points; 

    void Awake()
    {
        points = new Transform[resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(cube);
            position.x = (i + 0.5f) * step - 1f; // Convienient range for functions is -1 to 1
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }
    private void Update()
    {
        float t = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            if(functions == 0)
            {
                position.y = SineFunction(position.x, t);
            }
            if (functions == 1)
            {
                position.y = MultiSineFunction(position.x, t);
            }
            point.localPosition = position;
        }
    }
    public static float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }
    float MultiSineFunction(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + t)) / 2f;
        y *= 2f / 3f; // to guarantee -1 to 1 range.
        return y;
    }
}
