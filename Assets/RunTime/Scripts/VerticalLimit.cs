using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLimit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player;
        player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.Die();
        }
    }
}
