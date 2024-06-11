using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;
    [SerializeField] float bottomLimit;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, leftLimit, rightLimit),
            Mathf.Clamp(targetToFollow.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }
}
