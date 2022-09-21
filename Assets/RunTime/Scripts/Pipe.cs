using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform headPos;

    public Vector3 HeadPos => headPos.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(HeadPos, Vector3.one*0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player;
        player = other.GetComponent <PlayerController>();
        if(player != null)
        {
            player.Die();
        }
    }
}
