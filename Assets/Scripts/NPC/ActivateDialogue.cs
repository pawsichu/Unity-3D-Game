using UnityEngine;

public class ActivateDialogue : MonoBehaviour, Interactor.IInteractable
{
    [TextArea]
    public string dialogueText; // The dialogue text to display

    public void Interact()
    {
        DialogueManager.Instance.ShowDialogue(dialogueText);
        Debug.Log("Dialogue activated.");
    }
}
