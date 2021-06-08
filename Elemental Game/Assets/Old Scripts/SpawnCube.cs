using Chronos;
using UnityEngine;

public class SpawnCube : BaseBehaviour
{
    public GameObject cube;
    public Transform parent;
    public GameObject fallingCube;

    void Update()
    {
        if (!fallingCube)
        {
            fallingCube = Instantiate(cube, transform.position, Quaternion.identity, parent);
            fallingCube.GetComponent<Timeline>().rigidbody.useGravity = true;
        }
    }
}
