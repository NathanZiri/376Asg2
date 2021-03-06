using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public float minWait = 1f;
    public float maxWait = 4f;
    public GameObject floor = null;
    private NavMeshAgent navma = null;
    private Bounds _b;
    
    // Start is called before the first frame update
    void Awake()
    {
        floor = GameObject.Find("Plane");
        navma = this.GetComponent<NavMeshAgent>();
        _b = floor.GetComponent<Renderer>().bounds;
        
        Invoke("ChoseDestination", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (navma.hasPath == false || navma.remainingDistance < 1.0f)
        {
            float time = Random.Range(minWait, maxWait);
            Invoke("ChoseDestination", time);
        }
    }

    public void Relocate()
    {
        float xLoc = Random.Range(_b.min.x, _b.max.x);
        float zLoc = Random.Range(_b.min.z, _b.max.z);
        transform.position = new Vector3(xLoc, this.transform.position.y, zLoc);
    }
    
    private void ChoseDestination()
    {
        float xLoc = Random.Range(_b.min.x, _b.max.x);
        float zLoc = Random.Range(_b.min.z, _b.max.z);
        Vector3 newPos = new Vector3(xLoc, this.transform.position.y, zLoc);
        navma.SetDestination(newPos);
    }
}