using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundSensor : MonoBehaviour
{
    [Tooltip("The time from touching the ground to changing the value of the variable.")]
    [SerializeField] private float delay;

    public bool isGrounded = false;

    private bool isGroundedTrue = false;// if the variable is true and touches the ground

    /// <summary>
    /// Called when it exit with the ground.
    /// </summary>
    public event System.Action GroundExit;

    /// <summary>
    /// Called when the ground is touched.
    /// </summary>
    public event System.Action GroundEnter;
    private void OnTriggerEnter(Collider other)
    {
        GroundEnter?.Invoke();
        if (isGrounded)
        {
            isGroundedTrue = true;
        }
        else
        {
            isGrounded = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isGrounded)
        {
            GroundEnter.Invoke();
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GroundExit?.Invoke();
        Invoke("ExitGround", delay);
    }
    void ExitGround()
    {
        isGrounded = false;
        if (isGroundedTrue)//if he touched the ground twice
        {
            isGroundedTrue = false;
            
        }
        else
        {
            isGrounded = false;
        }
        
    }
}
