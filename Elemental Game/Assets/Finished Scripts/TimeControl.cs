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
        Clock clock = Timekeeper.instance.Clock("World");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            clock.LerpTimeScale(slowdownFactor, 0.4f);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            clock.LerpTimeScale(1, 0.4f);
        }
    }
}
