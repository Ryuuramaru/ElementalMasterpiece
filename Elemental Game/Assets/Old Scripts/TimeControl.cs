using Chronos;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeControl : MonoBehaviour
{
    public float slowdownFactor;

    private float slowmoDuration;
    private float initialDuration;

    public float maxDuration;
    public float slowmoCooldown;


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
            initialDuration = clock.unscaledTime;
        }

        slowmoDuration = clock.unscaledTime - initialDuration;
        if(slowmoDuration > maxDuration) clock.LerpTimeScale(1, 0.4f);

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            clock.LerpTimeScale(1, 0.4f);
        }
    }
}
