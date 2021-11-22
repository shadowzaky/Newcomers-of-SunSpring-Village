using System;
using UnityEngine;
public class CameraMovement: MonoBehaviour
{
    public Transform target;
    public float smoothing;

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            var position = target.position;
            var targetPosition = new Vector3(position.x, position.y, -10);
            transform.position = Vector3.Lerp(transform.position,
                targetPosition,
                smoothing);
        }
    }
}
