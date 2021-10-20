using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject NPCSpawn;
    public float respawnTime = 3.0f;
    public float despawnTime = 7.0f;
    private Bounds _b;
    // Start is called before the first frame update
    void Start()
    {
        _b = GameObject.Find("Plane").GetComponent<Renderer>().bounds;
        StartCoroutine(NPCWave());
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
        yield return new WaitForSeconds(despawnTime);
        Destroy(despawer);
    }
    
    IEnumerator NPCWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
