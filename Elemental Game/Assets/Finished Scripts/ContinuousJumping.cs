using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousJumping : MonoBehaviour
{
    private Rigidbody rb;
    public float crazyness;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(transform.up * crazyness, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * crazyness * 0.5f);
    }
}
