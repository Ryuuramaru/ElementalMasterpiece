using Chronos;
using UnityEngine;
using System.Collections;

public class PlayerTimeManager : BaseBehaviour
{
    private float initialTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ImaginaryFloor")) StartCoroutine(RewindPlayer(initialTime));
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            Clock clock = Timekeeper.instance.Clock("Player");
            initialTime = clock.unscaledTime;
        }
    }

    IEnumerator RewindPlayer(float initialTime)
    {
        Clock clock = Timekeeper.instance.Clock("Player");
        float timePassed = clock.unscaledTime - initialTime;

        clock.LerpTimeScale(0, 0.25f, false);
        yield return new WaitForSeconds(0.5f);

        clock.LerpTimeScale(-2f, 0.5f, false);
        yield return new WaitForSeconds(timePassed + 1f);

        clock.LerpTimeScale(0, 0.25f, false);
        yield return new WaitForSeconds(0.5f);

        clock.LerpTimeScale(1, 0.25f, false);

        yield break;
    }
}
