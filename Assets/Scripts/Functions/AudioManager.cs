using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    public AudioClip background;
    public AudioClip eatSFX;
    public AudioClip byeSFX;
    public AudioClip deathSFX;
    public AudioClip yaySFX;
    public AudioClip hitSFX;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}


