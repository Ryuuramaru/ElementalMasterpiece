using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraNew : MonoBehaviour
{
   
    public Transform player;
    void Start()
    {

    }
    void Update()
    {
        transform.position = player.transform.position;
    }
}
