using Chronos;
using UnityEngine;
using System.Collections;

public class PlayerTimeManager : BaseBehaviour
{
    public float initialTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ImaginaryFloor")) StartCoroutine(RewindPlayer());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            Clock clock = Timekeeper.instance.Clock("Root");
            initialTime = clock.unscaledTime;
        }
    }

    IEnumerator RewindPlayer()
    {
        Clock clock = Timekeeper.instance.Clock("Root");
        float timePassed = clock.unscaledTime - initialTime;

        clock.LerpTimeScale(0, 0.25f, false);
        yield return new WaitForSeconds(0.5f);

        clock.LerpTimeScale(-4f, 1.75f, false);
        yield return new WaitForSeconds(timePassed / 4 + 1.75f);

        clock.LerpTimeScale(0, 0.25f, false);
        yield return new WaitForSeconds(0.5f);

        clock.LerpTimeScale(1, 0.25f, false);

        yield break;
    }
}
