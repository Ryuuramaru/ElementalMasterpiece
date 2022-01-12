using System.Collections;
using System.Collections.Generic;
using Chronos;

using UnityEngine;

public class Abilities : MonoBehaviour
{

    public Rigidbody rb;
    public float force;
    public float maxDuration;
    public float cooldown;

    public float availableTime;
    private float timePassed;
    private float initialTime;

    public bool canFly;

    private bool fly = false;
    private bool countTime = false;
    private bool regenerateTime = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Clock clock = Timekeeper.instance.Clock("Player");

        if (Input.GetKeyDown(KeyCode.Space) && timePassed >= cooldown && canFly)
        {
            //StartCoroutine(TimePassed());
            fly = true;
            countTime = true;
            regenerateTime = false;

            initialTime = clock.time;
        }

        if (Input.GetKeyUp(KeyCode.Space) && countTime)
        {
            fly = false;
            countTime = false;
            timePassed = 0;
            regenerateTime = true;
        }

        if (countTime)
        {
            availableTime -= Mathf.Abs(clock.deltaTime);
            if (availableTime <= 0f)
            {
                fly = false;
                timePassed = 0;
                countTime = false;
                regenerateTime = true;
            }
        }
        if (regenerateTime)
        {
            if (availableTime <= maxDuration) availableTime += Mathf.Abs(clock.deltaTime);
            else
            {
                availableTime = maxDuration;
                timePassed = cooldown;
            }

            timePassed += Mathf.Abs(clock.deltaTime);

        }
        if (fly)
        {
            rb.AddForce(Vector3.up * force);
        }


    }


}
