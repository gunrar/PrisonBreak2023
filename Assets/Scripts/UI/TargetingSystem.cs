using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ReticleDisplay : MonoBehaviour
{
    public Transform player; // The player's Transform
    public GameObject reticle; // Your reticle GameObject
    public float maxDistance = 100f; // Fixed distance from the player

    public CameraFollow camFollow;

    void Update()
    {
        PositionReticle();
        maxDistance = camFollow.cam.orthographicSize - 10;
    }

    void PositionReticle()
    {
        // Calculate the reticle position
        Vector2 reticlePosition = player.position + player.up * maxDistance;

        // Update the reticle's position
        reticle.transform.position = reticlePosition;

        // Ensure the reticle is always visible
        reticle.SetActive(true);

        
        
    }
}


