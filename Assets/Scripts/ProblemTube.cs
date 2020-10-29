using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemTube : MonoBehaviour
{
    public int tubeId;

    void OnTriggerEnter2D (Collider2D collision)
    {
        // was it the player?
        if(collision.gameObject.CompareTag("Player"))
        {
            // tell the game manager that the player has entered the tube
            GameManager.instance.OnPlayerEnterTube(tubeId);
        }
    }
}