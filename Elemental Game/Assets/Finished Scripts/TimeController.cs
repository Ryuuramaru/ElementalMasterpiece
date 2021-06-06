using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowdownFactor;
    private float PlayerSpeed;
    public GameObject Player;

  
    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime *= slowdownFactor; //Prevents lag from occuring. Adjusts physics calculation per second so it isn't affected by time slowing down.
        Player.GetComponent<Rigidbody>().velocity *= slowdownFactor;
    }

    public void DoNormalmotion()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime /= slowdownFactor;
        Player.GetComponent<Rigidbody>().velocity /= slowdownFactor;

    }

    private void Update()
    {
        if (/*Player.GetComponent<AbilityLocalization>().CanSlowmotion &&*/Input.GetKeyDown(KeyCode.LeftShift)) DoSlowmotion();

        if (Input.GetKeyUp(KeyCode.LeftShift)) DoNormalmotion();
    }

}
