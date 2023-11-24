using UnityEngine;

public class SpawnPrefabAtMouse : MonoBehaviour
{
    public GameObject prefabToSpawn; // Drag your prefab here through the inspector

    void Update()
    {
        // Check if F key and 1 key are both being pressed
        if (Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 mousePos = Input.mousePosition; // Get the mouse position in screen space
            // Convert the mouse position to world space coordinates
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            // Ensure the prefab spawns at the correct z-axis, you might need to adjust this value
            worldPosition.z = 0;

            // Instantiate the prefab at the converted mouse position
            Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
        }
    }
}