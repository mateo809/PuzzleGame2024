using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private float displayTime = 3f;

    private int currentLine = 0;
    private float timer;
    private bool isShowing;

    private void Start()
    {
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isShowing)
        {
            ShowDialogue();
        }
    }

    private void ShowDialogue()
    {
        dialogueCanvas.SetActive(true);
        isShowing = true;
        timer = displayTime;
    }

    private void Update()
    {
        if (isShowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                dialogueCanvas.SetActive(false);
                isShowing = false;
            }
        }
    }
}
