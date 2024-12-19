using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerInteractionScript : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public string npcTag = "NPC";

    private GameObject currentNPC;
    private Coroutine typingCoroutine;
    private bool isDialogOpen = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentNPC != null && !isDialogOpen)
        {
            OpenDialog(currentNPC);
        }
        if (Input.GetMouseButtonDown(0) && isDialogOpen)
        {
            CloseDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(npcTag))
        {
            currentNPC = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(npcTag))
        {
            currentNPC = null;
            CloseDialog();
        }
    }

    private void OpenDialog(GameObject npc)
    {
        string npcDialog = npc.GetComponent<NPC>().dialog;

        dialogPanel.SetActive(true);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(npcDialog));

        isDialogOpen = true;
    }

    private IEnumerator TypeText(string textToType)
    {
        dialogText.text = "";
        foreach (char letter in textToType.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void CloseDialog()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
        isDialogOpen = false;
    }
}
