using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    void Awake()
    {
        instance = this;
    }

    public ScreenTint screenTint;
    public CameraConfiner cameraConfiner;

    string currentScene;
    AsyncOperation unload;
    AsyncOperation load;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(TintAndSwitchScene(to, targetPosition));
    }

    void SwitchScene(string to, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        Transform playerTransform = GameManager.instance.player.transform;
        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);
        GameManager.instance.character4D.transform.position = targetPosition;
    }

    IEnumerator TintAndSwitchScene(string to, Vector3 targetPosition)
    {
        screenTint.TintScreen();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        SwitchScene(to, targetPosition);

        while (load != null & unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTintScreen();
    }
}
