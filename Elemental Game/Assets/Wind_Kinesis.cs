using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Wind_Kinesis : MonoBehaviour
{
    public float force;
    public float range;
    public float cost_rise;
    public float cost_push;
    public float cost_pull;
    public Transform player_transform;
    public GameObject player;


    RaycastHit hit;

    void Start()
    {

    }

    void Update()
    {
        //push
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range) && player.GetComponent<Abilities>().availableTime >= cost_push)
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(player_transform.forward * force, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Rigidbody>().AddTorque(player_transform.right * 50f);

                //empties a portion of the flight bar
                player.GetComponent<Abilities>().availableTime -= cost_push;
            }
        }

        //pull
        if (Input.GetMouseButton(1) && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Pushable") && player.GetComponent<Abilities>().availableTime >= cost_pull)
            {
                /*if (hit.distance <= 5f) hit.collider.gameObject.GetComponent<Rigidbody>().position = Vector3.Lerp(childTransform.transform.position, hit.collider.gameObject.GetComponent<Rigidbody>().position, 0.1f);
                else*/
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(player_transform.forward * -1 * force, ForceMode.Force);
                player.GetComponent<Abilities>().availableTime -= cost_pull;
            }

        }

        //lift into air
        if (Input.GetButtonDown("Push") && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range * 0.5f) && player.GetComponent<Abilities>().availableTime >= cost_rise)
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force * 0.5f, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Rigidbody>().AddTorque(player_transform.right * 20f);
                player.GetComponent<Abilities>().availableTime -= cost_rise;
            }
        }
    }
}

