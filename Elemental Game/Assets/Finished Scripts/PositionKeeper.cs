using UnityEngine;
using UnityEditor;
using Chronos;

public class PositionKeeper : MonoBehaviour
{
    public Vector3 ogPos;
    private Vector3 ogRotate;

    public float moveSpeed;
    private Rigidbody rb;
    private Clock clock;

    public float lifeTime;
    private float ogTime;

    public Vector3 moveDir;

    void Start()
    {
        ogPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = moveDir * moveSpeed;

        clock = Timekeeper.instance.Clock("World");
        ogTime = clock.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + moveDir * moveSpeed * lifeTime);
    }

    void Update()
    {
        //DEBUG

        //Debug.DrawRay(ogPos, moveDir * lifeTime * moveSpeed, Color.magenta);

        clock = Timekeeper.instance.Clock("World");

        if(clock.time - ogTime >= lifeTime)
        {
            transform.position = ogPos;
            //clock = Timekeeper.instance.Clock("World");
            rb.velocity = moveDir * moveSpeed * clock.localTimeScale;
            ogTime = clock.time;
        }

        /*if (collision.gameObject.CompareTag("ImaginaryFloor"))
        {
            transform.position = ogPos;
            clock = Timekeeper.instance.Clock("World");
            rb.velocity = Vector3.down * moveSpeed * clock.localTimeScale;
        }

        else if(collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }*/
    }
}