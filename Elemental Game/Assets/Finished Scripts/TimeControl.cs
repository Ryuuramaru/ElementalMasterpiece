using Chronos;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float slowdownFactor;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Clock clock = Timekeeper.instance.Clock("Root");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            clock.localTimeScale = slowdownFactor;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            clock.localTimeScale = 1;
        }
    }
}
