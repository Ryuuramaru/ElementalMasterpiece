using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public float damage = 10f;
    public Camera cam;
    public GameObject projectile;
    private Vector3 destination;
    public Transform firePoint;

    public object Physic { get; private set; }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            GameObject fireball = Instantiate(projectile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
        }
    }
    void Shoot()
    {
        /*
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        */
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);
    }
  
    }
