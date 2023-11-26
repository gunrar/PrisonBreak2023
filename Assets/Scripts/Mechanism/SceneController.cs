using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    TerrainSpawnController TSC;
    SpawnControl SC;
    public GameObject player1;
    public GameObject player2;
    private Vector3 player1V3Start;
    private Vector3 player2V3Start;
    public bool startingScene = false;
    void Start()
    {
        player1V3Start = player1.transform.position;
        player2V3Start = player2.transform.position;
        TSC = GetComponent<TerrainSpawnController>();
        SC = GetComponent<SpawnControl>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SC.spawnerPause && !startingScene)
        {
            StartCoroutine(StartNewScene());
        }
    }

    public float sceneTransitionDelayTime;
    IEnumerator StartNewScene()
    {
        
        startingScene = true;
        TSC.ClearObjects();
        yield return new WaitForSeconds(SC.waveSpawnDelay);
        player1.transform.position = player1V3Start;
        player2.transform.position = player2V3Start;
        TSC.MasterSpawn();
        startingScene = false;
        SC.spawnerPause = false;
    }
}
