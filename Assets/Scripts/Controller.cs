using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region FIELDS

    private float horizontal, vertical;
    private Vector3 _moveDir;
    public float speed;
    public CharacterController charController;

    public Cinemachine.CinemachineFreeLook Camera;
    public Transform[] targets;
    #endregion

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        SwitchTargets();
        Movement();
    }
    void FixedUpdate()
    {
        charController.Move(_moveDir * Time.deltaTime * speed);
    }
    /// <summary>
    /// Calculates movement
    /// </summary>
    void Movement()
    {
        _moveDir = new Vector3(horizontal, 0f, vertical);
        _moveDir.Normalize();
    }
    void SwitchTargets()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            Camera.m_LookAt = targets[0].transform;
           // Camera.m_Follow = targets[0].transform;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Camera.m_LookAt = targets[1].transform;
            //Camera.m_Follow = targets[1].transform;
        }
    }
}
