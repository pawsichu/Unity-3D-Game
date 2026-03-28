using UnityEngine;

public class Points : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float pointAmount = 20f;
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            AudioManager audioManager = FindAnyObjectByType<AudioManager>();
            if (playerStats != null)
            {
                audioManager.SFXSource.PlayOneShot(audioManager.eatSFX);
                playerStats.Point(pointAmount);
                Destroy(gameObject);
            }
        }
    }
}
