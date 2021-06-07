using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bounceForce;
    [SerializeField] private float rotateForce;

    private void Start()
    {
        rb.AddTorque(Random.Range(0f, 1f) * rotateForce, Random.Range(0f, 1f) * rotateForce, Random.Range(0f, 1f) * rotateForce);
    }


    private void OnCollisionEnter()
    {
        Random.Range(0f, 1f);
        rb.AddForce(transform.up * bounceForce, ForceMode.VelocityChange);
        rb.AddTorque(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
    }
}
