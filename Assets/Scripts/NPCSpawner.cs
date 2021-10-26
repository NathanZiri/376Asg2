using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    //parameter instantiation
    public GameObject player;
    public GameObject NPCSpawn;
    public float minTime = 5.0f;
    public float maxTime = 25.0f;
    private Bounds _b;
    int speedMod = 0;
    int isSpedUp = 0;
    public int scoreMod = 0;
    
    // Start is called before the first frame update
    //starts coroutines for spawning in npcs
    void Start()
    {
        _b = GameObject.Find("Plane").GetComponent<Renderer>().bounds;
        StartCoroutine(NPCWave());
        StartCoroutine(WaveController());
        for(int i = 0 ; i < 5; i++)
            spawnEnemy();
        StartCoroutine(BigWave(30));
        StartCoroutine(BigWave(60));
    }

    private void Update()
    {
        //speedMod = GameObject.Find("Canvas").GetComponent<MenuManager>().easyMode ? 0 : 2;
    }
    //for setting if it is the easy or hard game mode
    public void ModifyMode()
    {
        scoreMod = 1;
    }
    
    //spawns in a singular npc and sets up to despawn it 
    private void spawnEnemy()
    {
        GameObject NPC = Instantiate(NPCSpawn) as GameObject;
        float xLoc = Random.Range(_b.min.x, _b.max.x);
        float zLoc = Random.Range(_b.min.z, _b.max.z);
        NPC.transform.position = new Vector3(xLoc, this.transform.position.y, zLoc);
        StartCoroutine(despawnEnemy(NPC));
    }

    
    //despawns the npc and deducts points when needed
    IEnumerator despawnEnemy(GameObject despawer)
    {
        
        yield return new WaitForSeconds(Random.Range(minTime+5,maxTime));
        
        
        if(player.GetComponent<PlayerController>().speedMod)
            isSpedUp = 1;
        
        if (!despawer.GetComponent<InteractableEnemy>().masked)
        {
            Debug.Log("nomask");
            player.GetComponent<PlayerController>().playerScore -= (2+scoreMod+isSpedUp);
        }
        if (despawer.GetComponent<InteractableEnemy>().type.Equals("I"))
        {
            Debug.Log("sick");
            player.GetComponent<PlayerController>().playerScore -= (4+scoreMod+isSpedUp);
        }
        
        Destroy(despawer);
    }
    
    //spawns a wave of multiple enemies
    IEnumerator BigWave(int waitTime){
        yield return new WaitForSeconds(waitTime);
        for(int i = 0; i < 8; i++)
            spawnEnemy();
    }
    
    //continually spans in npcs
    IEnumerator NPCWave(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0, minTime+5 - (2+speedMod)));
            Debug.Log("spawn");
            spawnEnemy();
        }
    }
    
    //slowly decreases the amount of time in between each call to deaspawn 
    IEnumerator WaveController()
    {
        
        bool rush = true;
        int interval = 25;
        while(true){
            yield return new WaitForSeconds(interval);
            if (minTime <= 2)
            {
                break;
            }
            if(rush){
                minTime -= (2 + speedMod);
                maxTime -= (2 + speedMod);
            }
            else
            {
                minTime += (1-speedMod);
                maxTime += (1-speedMod);
                interval -= 5;
            }

            rush = !rush;
        }
    }
}
