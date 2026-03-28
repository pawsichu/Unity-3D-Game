using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI pointText;

    [SerializeField] public float points = 0;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public HealthBar healthbar;
    [SerializeField] public int minDamage = 5;
    [SerializeField] public int maxDamage = 25;    

    public PlayerStats health;
    public PlayerStats point;

    public float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateHealthAndTextUI();
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.SFXSource.PlayOneShot(audioManager.hitSFX);
        currentHealth -= damage;
        Debug.Log(currentHealth);

        UpdateHealthAndTextUI();

        if (currentHealth <= 0f)
        {
            audioManager.SFXSource.PlayOneShot(audioManager.deathSFX);
            currentHealth = maxHealth;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Point(float pointAmount)
    {
        points += pointAmount;

        UpdateHealthAndTextUI();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthAndTextUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("is attacking");
            int damage = Random.Range(minDamage, maxDamage);
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyTakeDamage(damage);
            }
        }

    }

    private void UpdateHealthAndTextUI()
    {
        healthText.text = $" {currentHealth}/{maxHealth}";
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        pointText.text = $" x {points}";
    }

}