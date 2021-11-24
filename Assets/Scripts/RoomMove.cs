using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMove : Interactable
{
    public bool isExiting = false;
    public string sceneName;
    public Vector3 restorePosition;

    public override void Interact()
    {
        if (!isExiting)
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPositionPrefs.SaveCurrentPlayerPosition($"{currentSceneName}_RoomMove_PlayerPos", restorePosition);
        }
        SceneManager.LoadScene(sceneName);
    }
}
