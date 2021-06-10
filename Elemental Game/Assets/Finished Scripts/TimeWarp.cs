using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using Chronos;
public class TimeWarp : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    public float slowdownFactor;
    public float maxDuration;
    public float cooldown;

    public float availableTime;
    private float timePassed;
    private float initialTime;

    private bool canWarp;
    private bool countTime = false;
    private bool regenerateTime = true;


    void Start()
    {
        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);

        m_Vignette.intensity.Override(0f);

        m_Vignette.smoothness.Override(1f);

        m_Vignette.roundness.Override(0.9f);

        m_Vignette.color.Override(Color.blue);

        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);

        availableTime = maxDuration;
        
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
        Clock Wclock = Timekeeper.instance.Clock("World");

        Wclock.LerpTimeScale(slowdownFactor, 0.4f);

        //StopCoroutine(VignetteLerp(0f, 0f));
        StartCoroutine(VignetteLerp(0f, 0.4f));
    }

    void DoNormalTime()
    {
        Clock clock = Timekeeper.instance.Clock("World");

        clock.LerpTimeScale(1, 0.4f);

        //StopCoroutine(VignetteLerp(0f, 0f));
        StartCoroutine(VignetteLerp(0.4f, 0f));
    }

    IEnumerator VignetteLerp(float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < 0.4f)
        {
            m_Vignette.intensity.value = Mathf.Lerp(startValue, endValue, timeElapsed / 0.4f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}