using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int maxObjects = 5;
    public int i;
    //Ax+Bx+C

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SpawnObjects();
        }
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (i = 0; i < maxObjects; i++)
        {
            Instantiate(prefab, Polynomial(), Quaternion.identity);
            if(Polynomial().x > 5f)
            {
                prefab.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.green);
                Debug.Log("Detected");
            }
        }
    }
    private Vector3 Polynomial()
    {
        return new Vector3(Random.Range(-10,10), Random.Range(-10, 10), Random.Range(-10, 10));
    }
}
