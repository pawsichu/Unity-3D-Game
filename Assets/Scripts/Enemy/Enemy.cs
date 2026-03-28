using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] public HealthBar healthbar;

    [SerializeField] public int minDamage = 5;
    [SerializeField] public int maxDamage = 25;

    public float currentHealth;

    public Enemy health;
    public float damageCooldown = 0.5f;

    private float timer;
    private Transform target;

    private void Start()
    {
        UpdateHealth();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("is attacking");
            int damage = Random.Range(minDamage, maxDamage);
            // Try to get Health component from the object we hit
            PlayerStats playerHealth = collision.gameObject.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

    }
    public void EnemyTakeDamage(int damages)
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.SFXSource.PlayOneShot(audioManager.hitSFX);
        currentHealth -= damages;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died!");
            audioManager.SFXSource.PlayOneShot(audioManager.deathSFX);
            Destroy(gameObject);
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void UpdateHealth()
    {
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }
}