using System.Collections;
using Chronos;
using UnityEngine;
public class TimeWarp : MonoBehaviour
{
    public float slowdownFactor;
    public float maxDuration;
    public float cooldown;

    public float availableTime;
    private float timePassed;
    private float initialTime;

    public bool canWarp;
    private bool countTime = false;
    private bool regenerateTime = true;

    public GameObject playerCamera;


    void Start()
    {
        
    }

    void Update()
    {
        Clock clock = Timekeeper.instance.Clock("Player");

        if (Input.GetKeyDown(KeyCode.LeftShift) && timePassed >= cooldown)
        {
            //StartCoroutine(TimePassed());
            DoSlowTime();
            countTime = true;
            regenerateTime = false;

            initialTime = clock.time;
            timePassed = 0;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && countTime)
        {
            DoNormalTime();
            countTime = false;
            regenerateTime = true;
        }

        if (countTime)
        {
            availableTime -= Mathf.Abs(clock.deltaTime);
            if (availableTime <= 0f)
            {
                DoNormalTime();
                countTime = false;
                regenerateTime = true;
            }
        }
        if (regenerateTime)
        {
            if (availableTime <= maxDuration) availableTime += Mathf.Abs(clock.deltaTime);
            else availableTime = maxDuration;

            timePassed += Mathf.Abs(clock.deltaTime);
        }
    }

    /*IEnumerator TimePassed()
    {
        Clock clock = Timekeeper.instance.Clock("Player");
        float initialTime = clock.time;
        float timePassed = 0f;

        while(timePassed <= maxDuration)
        {
            Clock Pclock = Timekeeper.instance.Clock("Player");
            timePassed = Pclock.time - initialTime;
            if (Input.GetKeyUp(KeyCode.LeftShift)) yield return timePassed;
        }

        DoNormalTime();
        yield return maxDuration;  
    }*/

    void DoSlowTime()
    {
        Clock clock = Timekeeper.instance.Clock("World");

        clock.LerpTimeScale(slowdownFactor, 0.4f);

        playerCamera.GetComponent<Vignette_Cotroller>().VignetteLerp(0.4f, Color.blue, 0.4f);
    }

    void DoNormalTime()
    {
        Clock clock = Timekeeper.instance.Clock("World");

        clock.LerpTimeScale(1, 0.4f);

        playerCamera.GetComponent<Vignette_Cotroller>().VignetteLerp(0, Color.blue, 0.4f);
    }

}