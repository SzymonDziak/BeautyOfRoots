using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Big thanks to: Jasper Flick & Mathigon.org
 * for great documentation on fractals..
 */
public class Fractal : MonoBehaviour
{
    #region FIELDS

    /// <summary>
    /// Creates directions to guide the parent fractals.
    /// So they don't overrun each other
    /// </summary>
    private static readonly Vector3[] childDirections =
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
        Vector3.down
    };
    /// <summary>
    /// Creates rotation or orientation for child fractals.
    /// So they don't overlap.
    /// Similar to childDirections.
    /// </summary>
    private static readonly Quaternion[] childOrientations =
    {
        Quaternion.identity, // Fractal Child Up
        Quaternion.Euler(0f, 0f, -90f), // Fractal Child Right
        Quaternion.Euler(0f, 0f, 90f), // Fractal Child Left
        Quaternion.Euler(90f, 0f, 0f), // Fractal Child Foward
        Quaternion.Euler(-90f, 0f, 0f), // Fractal Child Back
        Quaternion.Euler(0f, 0f, 270f) // Fractal Child Down
    };

    [SerializeField]
    private int depth;
    private Material[] materials;

    [Header("Type of Object")]
    public Mesh mesh;
    public Material material;

    [Header("Variables")]
    public int maxDepth; // max depth of fractal
    public float childScale; // the scale at which the fractals decrease

    [Header("Materials")]
    public Color[] ColorToLerp = new Color[2];
    public Color OuterColor;

    public static int count;
    public bool enableSineFunction;
    public static Fractal Instance { get; set; }

    Transform[] points;

    #endregion

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1];
        for (int i = 0; i <= maxDepth; i++)
        {
            materials[i] = new Material(material)
            {
                color =
                Color.Lerp(ColorToLerp[0], ColorToLerp[1], (float)i / maxDepth)
            };
        }
        materials[maxDepth].color = OuterColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        count++;

        // Only create materials once... 
        if(materials == null)
        {
            InitializeMaterials();
            Debug.Log("Initializing materials");
        }

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().sharedMaterial = materials[depth];

        if(depth < maxDepth)
        {
            StartCoroutine(CreateFractals());
        }
    }
    private IEnumerator CreateFractals()
    {
        for(int i = 0; i < childDirections.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            new GameObject("Fractal Child", typeof(SineFunction)).AddComponent<Fractal>()
                .Initialize(this, i);
            // typeof(SineFunction)
        }

    }
    private void Initialize(Fractal parent, int childIndex)
    {
        mesh = parent.mesh;
        materials = parent.materials;
        depth = parent.depth + 1;
        maxDepth = parent.maxDepth;

        childScale = parent.childScale;
        transform.parent = parent.transform; // sets the hierarchy for each new instantiation
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[childIndex]; // rotates in direction away from fractal.
    }
}