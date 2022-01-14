
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Wind_Kinesis : MonoBehaviour
{
    Vignette m_Vignette;
    PostProcessVolume m_Volume;

    public float force;
    public float range;
    public float cost_rise;
    public float cost_push;
    public float cost_pull;
    public Transform player_transform;
    public GameObject player;
    //public GameObject player_camera;
    public float lerp_time;


    RaycastHit hit;

    void Start()
    {
        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();

        m_Vignette.enabled.Override(true);

        m_Vignette.intensity.Override(0f);

        m_Vignette.smoothness.Override(1f);

        m_Vignette.roundness.Override(0.9f);

        m_Vignette.color.Override(Color.gray);

        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        //push
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range) && player.GetComponent<Abilities>().availableTime >= cost_push)
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(player_transform.forward * force, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Rigidbody>().AddTorque(player_transform.right * 50f);

                //empties a portion of the flight bar
                player.GetComponent<Abilities>().availableTime -= cost_push;

                //lerps the screen to a grey color. to implement charging up of ability
                //StartCoroutine(VignetteLerp(0.4f, 0f));
                Yes();
            }
        }

        //pull
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range) && player.GetComponent<Abilities>().availableTime >= cost_pull)
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                /*if (hit.distance <= 5f) hit.collider.gameObject.GetComponent<Rigidbody>().position = Vector3.Lerp(childTransform.transform.position, hit.collider.gameObject.GetComponent<Rigidbody>().position, 0.1f);
                else*/
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(player_transform.forward * -1f * force, ForceMode.Impulse);
                player.GetComponent<Abilities>().availableTime -= cost_pull;
            }

        }

        //lift into air
        if (Input.GetButtonDown("Push") && Physics.Raycast(player_transform.position, player_transform.forward, out hit, range * 0.5f) && player.GetComponent<Abilities>().availableTime >= cost_rise)
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force * 0.5f, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Rigidbody>().AddTorque(player_transform.right * 20f);
                player.GetComponent<Abilities>().availableTime -= cost_rise;
            }
        }
    }
    void Yes()
    {
        StartCoroutine(VignetteLerp(0f, 1f));
        Internal.Debug.Log("?");
    }
    IEnumerator VignetteLerp(float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < 0.2f)
        {
            m_Vignette.intensity.value = Mathf.Lerp(startValue, endValue, timeElapsed / 0.4f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}

