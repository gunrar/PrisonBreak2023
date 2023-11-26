using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, GameObject> playerGameObjects = new Dictionary<int, GameObject>();

    // Call this method when you instantiate a player GameObject
    public void RegisterPlayerGameObject(Player player, GameObject playerGameObject)
    {
        playerGameObjects[player.ActorNumber] = playerGameObject;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // The new player has connected, but their GameObject might not be instantiated yet
        // You can check for or wait for their GameObject using newPlayer.ID
        StartCoroutine(WaitForPlayerGameObject(newPlayer.ActorNumber));
    }

    private IEnumerator<int> WaitForPlayerGameObject(int playerId)
    {
        while (!playerGameObjects.ContainsKey(playerId))
        {
            yield return null; // Wait for the next frame
        }

        // Now you have the new player's GameObject
        GameObject newPlayerGameObject = playerGameObjects[playerId];
        // Perform actions with newPlayerGameObject
        Debug.Log(playerId);    
    }
}