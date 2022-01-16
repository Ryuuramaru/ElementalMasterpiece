using Chronos;
using UnityEngine;

public class Enemy_Shoot_Fireball : MonoBehaviour
{

    public GameObject Fireball;

    public GameObject Player;

    public float cooldown = 5f;
    public float speed = 5f;

    private float timepassed = 0f;

    public bool canShoot;

    Clock clock;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        clock = Timekeeper.instance.Clock("World"); //scales time with Chronos

        //cooldown between shots
        if (timepassed < cooldown)
        {
            timepassed += clock.deltaTime;
        }
        else if (canShoot)
        {
            timepassed = 0f;
            Transform fireball_transform;

            //calculates the trajectory it should take
            UnityEngine.Vector3 trajectory = Player.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;

            //calculates the rotation / the direction the fireball should face
            Quaternion rotation = Quaternion.LookRotation(trajectory, Vector3.up);

            //spawns the fireball and fires it at the player
            GameObject Fireball_Instance = Instantiate(Fireball, gameObject.GetComponent<Transform>().position + new Vector3(0, 30, 0), rotation);
            fireball_transform = Fireball_Instance.GetComponent<Transform>();
            //shoots too far up
            Fireball_Instance.GetComponent<Rigidbody>().AddForce(speed * 1000 * fireball_transform.forward);
        }
    }
}
