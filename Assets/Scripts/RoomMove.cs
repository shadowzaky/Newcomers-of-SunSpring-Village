using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMove : MonoBehaviour
{
    public bool isExiting = false;
    public string sceneName;
    public Vector3 restorePosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isExiting)
            {
                var currentSceneName = SceneManager.GetActiveScene().name;
                PlayerPositionPrefs.SaveCurrentPlayerPosition($"{currentSceneName}_RoomMove_PlayerPos", restorePosition);
            }
            SceneManager.LoadScene(sceneName);
        }    
    }
}
