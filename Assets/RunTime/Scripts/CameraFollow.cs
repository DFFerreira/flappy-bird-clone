using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Vector3 cameraPos;

    private void Awake()
    {
        cameraPos = transform.position;
    }

    private void LateUpdate()
    {
        cameraPos.x = player.transform.position.x;

        transform.position = cameraPos;
    }
    
}
