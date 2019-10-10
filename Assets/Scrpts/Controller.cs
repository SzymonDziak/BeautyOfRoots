using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController charController;
    private Vector3 movement;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        charController = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        charController.Move(movement * speed * Time.deltaTime);
    }
}
