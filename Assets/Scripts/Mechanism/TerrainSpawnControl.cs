using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
public class TerrainSpawnController : MonoBehaviour
{
    private GameObject[] terrainSpawnPoints;


    [System.Serializable]
    public struct TerrainObject
    {
        public GameObject prefab;
        public int size;
    }

    public List<TerrainObject> terrainObjects;
    public float chanceToSpawnTerrain;

    void Start()
    {
        MasterSpawn();

    }

    public void MasterSpawn()
    {
        SelectLayout();
        FindTerrainSpawnPoints();
        SpawnTerrain();
        // Example: Iterate through each pair and log their IDs and names
    }

    private void FindTerrainSpawnPoints()
    {
        // Find and store all game objects tagged as "TerrainSpawnPoint" in the array
        terrainSpawnPoints = GameObject.FindGameObjectsWithTag("TerrainSpawnPoint");

        // Optional: Print the count of spawn points found
        //Debug.Log("Found " + terrainSpawnPoints.Length + " terrain spawn points.");
    }

    public GameObject[] terrainLayouts;
    private void SelectLayout()
    {
        int i = Random.Range(0, terrainLayouts.Length);
        terrainLayouts[i].SetActive(true);
    }

    private void SpawnTerrain()
    {
       
        for (int i = 0; i < terrainSpawnPoints.Length; i++)
        {
            
            int y = 0;
            List<GameObject> potentialTerrainObjects = new  List<GameObject>();
            for (int j = 0; j <terrainObjects.ToArray().Length; j++)
            {

                if (terrainObjects.ToArray()[j].size == terrainSpawnPoints[i].GetComponent<TerrainSpawner>().terrainSize)
                {
                    

                    potentialTerrainObjects.Add(terrainObjects.ToArray()[j].prefab);
                    y++;
                }

                for (int z = 0; z < potentialTerrainObjects.ToArray().Length; z++)
                {
                    
                    if ((chanceToSpawnTerrain/100)/potentialTerrainObjects.ToArray().Length > Random.Range(0f,1f))
                    {

                        //TerrainObject selectedEnemyType = terrainObjects[Random.Range(0, terrainObjects.Count)];
                        GameObject levelePiece = PhotonNetwork.Instantiate(potentialTerrainObjects[z].name, terrainSpawnPoints[i].transform.position, Quaternion.identity);
                        //GameObject levelePiece = Instantiate(potentialTerrainObjects[z], terrainSpawnPoints[i].transform.position, Quaternion.identity);
                        levelObjects.Add(levelePiece);
                    }


                }
                potentialTerrainObjects.Clear();

            }
            

            
        }
    }

    public List<GameObject> levelObjects;
    public void ClearObjects()
    {
        for (int i = 0; i < terrainLayouts.Length; i++)
        {
            terrainLayouts[i].SetActive(false);
        }
        int x = levelObjects.ToArray().Length;
        Debug.Log(x);
        for (int i = 0; i < x; i++)
        {
            Destroy(levelObjects.ToArray()[i]);
        }
        levelObjects.Clear();
    }


    

    // You can add other methods here to interact with the spawn points
}
