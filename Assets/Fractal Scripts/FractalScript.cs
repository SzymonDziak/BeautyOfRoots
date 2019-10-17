using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalScript : MonoBehaviour
{
    [Header("Properties")]
    public Mesh mesh;
    public Material material;
    public int maxDepth;
    public float childScale;

    private int depth;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        if(depth < maxDepth)
        {
            new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.up);
        }
    }
    private void Initialize(FractalScript parent, Vector3 direction)
    {
        // Uses preassigned parent paramaters
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1; // so the loop doesn't go on forever... 
        childScale = parent.childScale;

        transform.parent = parent.transform; // sets the hierarchy for new objects

        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + (0.5f * childScale));

        // transform.localRotation = orientation;
    }
    //new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.right);
    //new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.left);
    //new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.down);
    // For 3D:
    // new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.up, Quaternion.identity);
    // new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.right, Quaternion.Euler(0, 0, -90f));
    // new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.left, Quaternion.Euler(0, 0, 90f));
    // new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.forward, Quaternion.Euler(90f, 0, 0));
    // new GameObject("Fractal Parent").AddComponent<FractalScript>().Initialize(this, Vector3.backward, Quaternion.Euler(-90f, 0, 0));
}
