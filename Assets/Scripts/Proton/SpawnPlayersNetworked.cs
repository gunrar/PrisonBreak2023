using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayersNetworked : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public List<GameObject> playerList = new List<GameObject>();

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Dictionary to store player colors based on their Photon Player ID
    private Dictionary<int, Color> playerColors = new Dictionary<int, Color>();
    private Player currentPlayer;

    private void Start()
    {
        SpawnPlayer();
    }

    //void SpawnPlayer()
    //{
    //    // Instantiate the player prefab across the network
    //    GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    //}

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    // Add the new player to the list
    //    Debug.Log("Player has joined");
    //    playerList.Add(newPlayer);
    //    SpawnPlayer();
    //}

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    // Remove the disconnected player from the list
    //    Debug.Log("Player has left");
    //    playerList.Remove(otherPlayer);
    //}

    void SpawnPlayer()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        AssignPlayerColor(player);
        playerList.Add(player);
    }

    // Method to assign a random color to the player
    // Method to assign or retrieve the color for the player
    void AssignPlayerColor(GameObject player)
    {
        // Check if the color is already assigned for the player
        if (currentPlayer != null && playerColors.ContainsKey(currentPlayer.ActorNumber))
        {
            // Retrieve the color from the dictionary
            Color storedColor = playerColors[currentPlayer.ActorNumber];

            // Set the color on the player's material or sprite renderer
            Renderer renderer = player.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = storedColor;
            }
            else
            {
                SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = storedColor;
                }
            }
        }
        else
        {
            // Generate a random color if not already assigned
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Set the color on the player's material or sprite renderer
            Renderer renderer = player.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = randomColor;
            }
            else
            {
                SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = randomColor;
                }
            }

            // Store the color in the dictionary for future reference
            if (currentPlayer != null)
            {
                playerColors[currentPlayer.ActorNumber] = randomColor;
            }
        }
    }
}
