using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject NPCSpawn;
    public float minTime = 5.0f;
    public float maxTime = 25.0f;
    private Bounds _b;
    // Start is called before the first frame update
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

    private void spawnEnemy()
    {
        GameObject NPC = Instantiate(NPCSpawn) as GameObject;
        float xLoc = Random.Range(_b.min.x, _b.max.x);
        float zLoc = Random.Range(_b.min.z, _b.max.z);
        NPC.transform.position = new Vector3(xLoc, this.transform.position.y, zLoc);
        StartCoroutine(despawnEnemy(NPC));
    }

    IEnumerator despawnEnemy(GameObject despawer)
    {
        yield return new WaitForSeconds(Random.Range(minTime+10,maxTime));
        if (!despawer.GetComponent<InteractableEnemy>().masked)
        {
            player.GetComponent<PlayerController>().playerScore -= 1;
        }
        if (!despawer.GetComponent<InteractableEnemy>().type.Equals("I"))
        {
            player.GetComponent<PlayerController>().playerScore -= 3;
        }
        
        Destroy(despawer);
    }
    
    IEnumerator BigWave(int waitTime){
        yield return new WaitForSeconds(waitTime);
        for(int i = 0; i < 6; i++)
            spawnEnemy();
    }
    
    IEnumerator NPCWave(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0, minTime));
            Debug.Log("spawn");
            spawnEnemy();
        }
    }
    
    IEnumerator WaveController()
    {
        bool rush = true;
        int interval = 25;
        while(true){
            yield return new WaitForSeconds(interval);
            Debug.Log("timeChange");
            if (minTime <= 2)
            {
                break;
            }
            if(rush){
                minTime -= 2;
                maxTime -= 2;
            }
            else
            {
                minTime += 1;
                maxTime += 1;
                interval -= 5;
            }

            rush = !rush;
        }
    }
}
