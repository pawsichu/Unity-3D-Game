using UnityEngine;

public class Destroy : MonoBehaviour, Interactor.IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}