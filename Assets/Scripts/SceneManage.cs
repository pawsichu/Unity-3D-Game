
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            changeScene();
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Level 1 loaded");
    }
}

