using UnityEditor;
using UnityEngine;

public class PointRequirement : MonoBehaviour
{
    [SerializeField] public int requiredPoints = 18;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            SceneManage sceneManager = other.GetComponent<SceneManage>();

            if (playerStats != null && sceneManager != null && playerStats.points >= (requiredPoints))
            {
                Debug.Log("Player has enough points to change the scene.");
                sceneManager.changeScene();
            }
        }
    }
}

