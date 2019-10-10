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
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            point.localPosition = position;
        }
    }
}
