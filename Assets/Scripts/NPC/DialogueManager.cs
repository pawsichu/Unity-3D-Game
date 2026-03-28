using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogue(string dialogue)
    {
        Debug.Log($"Dialogue: {dialogue}");
        // Replace this with your UI logic to display dialogue
    }
}
