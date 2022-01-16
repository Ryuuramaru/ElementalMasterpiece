using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public GameObject player;
    public float cooldown;
    public float speed;
    public float damage;
    private float timeElapsed = 0f;

    Clock clock;

    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        clock = Timekeeper.instance.Clock("World");
        if (time < cooldown)
        {
            time += clock.deltaTime;
        }
        else
        {
            time = 0;
            Attack();
        }
    }

    /*void OnCollisionStay(Collision other)
    {
        clock = Timekeeper.instance.Clock("World");
        if (other.gameObject.CompareTag("Floor"))
        {
            if (timeElapsed < cooldown)
            {
                timeElapsed += clock.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                Attack();
            }
        }
    }*/
    void Attack()
    {
        /*Vector3 cubePos = gameObject.GetComponent<Rigidbody>().position;
        Vector3 playerPos = player.GetComponent<Rigidbody>().position;
        float distance = Vector3.Distance(cubePos, playerPos);
        Vector3 trajectory = Vector3.MoveTowards(cubePos, playerPos, distance);
        gameObject.GetComponent<Rigidbody>().AddForce(trajectory * speed, ForceMode.Impulse);*/

        Vector3 cubePos = gameObject.GetComponent<Rigidbody>().position;
        Vector3 playerPos = player.GetComponent<Rigidbody>().position;

        Vector3 trajectory = playerPos - cubePos;

        //trajectory.Normalize();

        gameObject.GetComponent<Rigidbody>().AddForce(trajectory * speed, ForceMode.Impulse);

    }
}
