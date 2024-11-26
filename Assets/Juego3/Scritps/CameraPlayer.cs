using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform playerTransform; 

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
    }
}
