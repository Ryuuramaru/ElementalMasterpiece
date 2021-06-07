using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform cube;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Instantiate(cube, pos, Quaternion.identity);
    }
}
