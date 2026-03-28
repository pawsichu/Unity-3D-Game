using UnityEngine;

public class MoveOrNot : MonoBehaviour, Interactor.IInteractable
{

    public void Interact()
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();

        if (GetComponent<FollowPlayer>().enabled)
        {
            
            audioManager.SFXSource.PlayOneShot(audioManager.byeSFX);
            GetComponent<FollowPlayer>().enabled = false;
        }
        else
        {
            GetComponent<FollowPlayer>().enabled = true;
            audioManager.SFXSource.PlayOneShot(audioManager.yaySFX);
        }
    }

}
