using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public float minWait = 2f;
    public float maxWait = 5f;
    public GameObject floor = null;
    private NavMeshAgent navma = null;
    private Bounds _b;
    
    
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("Plane");
        navma = this.GetComponent<NavMeshAgent>();
        _b = floor.GetComponent<Renderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (navma.hasPath == false || navma.remainingDistance < 1.0f)
        {
            float time = Random.Range(minWait, maxWait);
            this.GetComponent<Renderer>().material.color = Color.red;
            Invoke("ChoseDestination", time);
        }
    }
    
    private void ChoseDestination()
    {
        float xLoc = Random.Range(_b.min.x, _b.max.x);
        Debug.Log(_b.min.x + "    " + _b.min.x);
        Debug.Log(_b.min.z + "    " + _b.min.z);
        float zLoc = Random.Range(_b.min.z, _b.max.z);
        Vector3 newPos = new Vector3(xLoc, this.transform.position.y, zLoc);
        navma.SetDestination(newPos);
        this.GetComponent<Renderer>().material.color = Color.green;
    }
}