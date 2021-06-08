using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class PositionKeeper : MonoBehaviour
{
    private Vector3 ogPos;
    private Vector3 ogRotate;

    public float downSpeed;
    private Rigidbody rb;
    private Clock clock;

    void Start()
    {
        ogPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * downSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ImaginaryFloor"))
        {
            transform.position = ogPos;
            clock = Timekeeper.instance.Clock("World");
            rb.velocity = Vector3.down * downSpeed * clock.localTimeScale;
        }

        else if(collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        clock = Timekeeper.instance.Clock("World");
        rb.velocity = Vector3.down * downSpeed * clock.localTimeScale;
    }
}
