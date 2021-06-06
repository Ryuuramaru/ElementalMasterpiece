using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLocalization : MonoBehaviour
{
    public bool CanRun = false;
    public bool CanJump = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Jump"))
            CanJump = true;
        if (other.CompareTag("Run"))
            CanRun = true;
    }

    public void OnTriggerExit(Collider other)
    {
        CanRun = false;
        CanJump = false;
    }

}
