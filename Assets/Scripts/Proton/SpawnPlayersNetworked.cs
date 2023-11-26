using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayersNetworked : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public PlayerManager playerManager;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        // Check if this is the local player's client
        if (photonView.IsMine)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            Player photonPlayer = player.GetPhotonView().Owner;
            playerManager.RegisterPlayerGameObject(photonPlayer, player);
        }
    }
}
