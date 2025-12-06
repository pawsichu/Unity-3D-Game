using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip eatSFX;
    public AudioClip walkSFX;
    public AudioClip deathSFX;
    public AudioClip interactSFX;
    public AudioClip buttonSFX;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}


