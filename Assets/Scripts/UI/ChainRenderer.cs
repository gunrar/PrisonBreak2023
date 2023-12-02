using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainRenderer : MonoBehaviour
{
    public GameObject startObject; // Assign in inspector or via code
    public GameObject endObject;   // Assign in inspector or via code

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Optional: Configure the appearance of the line
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        startObject = transform.gameObject;
        endObject = GetComponent<PlayerControllerNetworked>().otherPlayer;
        if (startObject != null && endObject != null)
        {
            // Update the positions of the line to match the GameObjects
            lineRenderer.SetPosition(0, startObject.transform.position);
            lineRenderer.SetPosition(1, endObject.transform.position);
        }
    }
}
