using UnityEngine;

public class InteractableTable:Interactable
{
    public GameObject sick;
    public bool isSick;

    private float minWait = 3f;
    private float maxWait = 8f;
    
    void Start()
    {
        isSick = Random.Range(0, 2) == 1;
        sick.SetActive(isSick);
        if (!isSick)
        {
            float time = Random.Range(minWait, maxWait);
            Invoke("DirtyTable", time);
        }
    }
    void Update()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius);
        foreach(var hitEnemy in hitEnemies){
            InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
            if (intr != null)
            {
                intr.modifyType(false);
            }
        }
    }

    public void DirtyTable()
    {
        isSick = true;
        sick.SetActive(isSick);
    }
    
    public int CleanTable()
    {
        
        if (isSick)
        {
            isSick = false;
            sick.SetActive(isSick);
            float time = Random.Range(minWait, maxWait);
            Invoke("DirtyTable", time);
            return 5;
        }
        else
        {
            return 0;
        }
    }
    
}