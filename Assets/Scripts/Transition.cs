using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Warp,
    Scene
}

public class Transition : MonoBehaviour
{
    public TransitionType transitionType;
    public string sceneNameToTransition;
    public Vector3 targetPosition;
    Transform desitination;

    void Start()
    {
        desitination = transform.GetChild(1);
    }

    public void InitiateTransition(Transform toTransform)
    {
        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        float oldSmoothing = cameraMovement.smoothing;
        cameraMovement.smoothing = 0;
        switch (transitionType)
        {
            case TransitionType.Warp:
                toTransform.position = new Vector3(desitination.position.x, desitination.position.y, toTransform.position.z);
                break;
            case TransitionType.Scene:
                GameSceneManager.instance.SwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
        cameraMovement.smoothing = oldSmoothing;
    }
}
