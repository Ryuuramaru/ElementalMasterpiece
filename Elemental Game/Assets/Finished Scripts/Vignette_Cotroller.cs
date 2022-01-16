using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class Vignette_Cotroller : MonoBehaviour
{
    Vignette m_Vignette;
    PostProcessVolume m_Volume;
    Clock clock;
    // Start is called before the first frame update
    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();

        m_Vignette.enabled.Override(true);

        m_Vignette.intensity.Override(0f);

        m_Vignette.smoothness.Override(1f);

        m_Vignette.roundness.Override(0.9f);

        // Use the QuickVolume method to create a volume with a priority of 100, 
        //and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);

        clock = Timekeeper.instance.Clock("Player");
    }

    // Update is called once per frame
    public void VignetteLerp(float value, Color color, float duration, bool normalize = false)
    {
        m_Vignette.color.Override(color);
        StartCoroutine(VignetteLerpCoroutine(value, duration, normalize));
    }
    IEnumerator VignetteLerpCoroutine(float value, float duration, bool normalize = false)
    {
        float timeElapsed = 0;
        float startValue = m_Vignette.intensity.value;

        while (timeElapsed < duration)
        {
            m_Vignette.intensity.value = Mathf.Lerp(startValue, value, timeElapsed / duration);
            timeElapsed += clock.deltaTime;

            yield return null;
        }
        if (normalize)
        {
            timeElapsed = 0;

            while (timeElapsed < duration)
            {
                m_Vignette.intensity.value = Mathf.Lerp(value, 0, timeElapsed / duration);
                timeElapsed += clock.deltaTime;

                yield return null;
            }
        }
    }
}
