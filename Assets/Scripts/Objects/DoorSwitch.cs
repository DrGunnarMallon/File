using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Light2D switchLight;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private bool switchOn = true;
    [SerializeField] private bool isDoorEnabled = true;
    [SerializeField] private string openMessage;
    [SerializeField] private string closeMessage;

    [SerializeField] private GameObject[] toActivate;
    [SerializeField] private bool switchOff = false;

    private void Awake()
    {
        if (switchLight == null)
        {
            switchLight = GetComponentInChildren<Light2D>(true);
        }
    }

    private void Start()
    {
        if (switchOff) return;

        foreach (GameObject obj in toActivate)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (switchOn)
            {
                ToggleDoor();
                switchOn = false;
                if (switchOff) { disableToActivate(); } else { enableToActivate(); }
                switchLight.enabled = false;
                // FindFirstObjectByType<MessageUI>().DisplayMessage(openMessage);
            }
            else
            {
                ToggleDoor();
                if (switchOff) { enableToActivate(); } else { disableToActivate(); }
                switchLight.enabled = true;
                switchOn = true;
                // FindFirstObjectByType<MessageUI>().DisplayMessage(closeMessage);
            }
        }
    }

    private void enableToActivate()
    {
        foreach (GameObject obj in toActivate)
        {
            obj.SetActive(true);
        }
    }

    private void disableToActivate()
    {
        foreach (GameObject obj in toActivate)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // FindFirstObjectByType<MessageUI>()?.HideMessage();
        }
    }

    private void ToggleDoor()
    {
        if (doorObject == null) { return; }

        doorObject.SetActive(!isDoorEnabled);

        isDoorEnabled = !isDoorEnabled;

    }
}