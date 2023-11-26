using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayersNetworked : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    //public List<Player> playerList = new List<Player>();

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private PhotonView photonView;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
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
}
