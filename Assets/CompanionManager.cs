using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
public class CompanionManager : MonoBehaviour
{


    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public GameObject companionPrefab;

    public void SpawnCompanion(GameObject newPlayer)
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX) + newPlayer.transform.position.x, Random.Range(minY, maxY) + newPlayer.transform.position.y);
        GameObject companion = PhotonNetwork.Instantiate(companionPrefab.name, randomPosition, Quaternion.identity);
        companion.GetComponent<Companion>().playerObject = newPlayer;
        
        

    }


}
