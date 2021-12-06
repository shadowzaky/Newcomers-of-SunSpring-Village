using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    void Awake()
    {
        instance = this;
    }

    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void SwitchScene(string to, Vector3 targetPosition)
    {
        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        GameManager.instance.character4D.transform.position = targetPosition;
    }
}
