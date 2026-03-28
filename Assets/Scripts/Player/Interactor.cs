using UnityEngine;

public class Interactor : MonoBehaviour
{
    public interface IInteractable
    {
       public void Interact();
    }

    public Transform InteractorSource;
    public float InteractRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log($"Hit: {hitInfo.collider.gameObject.name}");
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("Interactable object found!");
                    interactObj.Interact();
                }
            }
        }


    }

}
