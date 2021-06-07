using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public Transform cube;
    public Transform parent;
    public bool canSpawn = true;

    void Update()
    {
        Vector3 pos = transform.position;
        //if(canSpawn) 
        Instantiate(cube, pos, Quaternion.identity, parent);
        canSpawn = false;
    }
}
