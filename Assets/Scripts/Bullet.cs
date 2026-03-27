using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletObject;
    public float shootingCoolDown = 0.5f;
    public float shootSpeed = 40f;

    private float timer;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
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
}
