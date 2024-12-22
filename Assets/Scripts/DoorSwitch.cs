using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Light2D switchLight;
    [SerializeField] private GameObject doorObject;

    private bool switchOn = true;
    private bool isDoorEnabled = true;

    private void Awake()
    {
        if (switchLight == null)
        {
            switchLight = GetComponentInChildren<Light2D>(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (switchOn)
            {
                Debug.Log("Switch light off");
                ToggleDoor();
                switchOn = false;
                switchLight.enabled = false;
                FindFirstObjectByType<MessageUI>().DisplayMessage("You hear a door rumble in the distance.");
            }
            else
            {
                Debug.Log("Switch light on");
                ToggleDoor();
                switchLight.enabled = true;
                switchOn = true;
                FindFirstObjectByType<MessageUI>().DisplayMessage("A door shuts closed *bang*");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<MessageUI>()?.HideMessage();
        }
    }

    private void ToggleDoor()
    {
        if (doorObject == null) { return; }

        doorObject.SetActive(!isDoorEnabled);

        isDoorEnabled = !isDoorEnabled;

    }
}
