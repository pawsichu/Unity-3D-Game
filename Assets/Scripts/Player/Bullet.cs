using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int damages = 10;

    public GameObject bulletObject;
    public float shootingCoolDown = 0.5f;
    public float shootSpeed = 40f;

    private float timer;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Vector3 direction = transform.forward;
        GameObject bullet = Instantiate(bulletObject, transform.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = direction * shootSpeed;

        Destroy(bullet, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("is attacking");
            int damage = Random.Range(10, 30);
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyTakeDamage(damage);
            }
        }

    }
}
