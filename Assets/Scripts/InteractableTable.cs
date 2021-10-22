using UnityEngine;

public class InteractableTable:Interactable
{
    public GameObject sick;
    public bool isSick;
    public GameObject player;

    private float minWait = 6f;
    private float maxWait = 10f;
    
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
        
        if(isSick)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitEnemy in hitEnemies)
            {
                InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
                if (intr != null)
                {
                    intr.modifyType(false);
                }
            }
        }
    }

    public void DirtyTable()
    {
        isSick = true;
        sick.SetActive(isSick);
		Invoke("LateDisinfect", 6);
    }
    
	private void LateDisinfect()
    {
        if (isSick)
            player.GetComponent<PlayerController>().playerScore -= 2;
    }

    public int CleanTable()
    {
        
        if (isSick)
        {
            isSick = false;
            sick.SetActive(isSick);
            float time = Random.Range(minWait, maxWait);
            Invoke("DirtyTable", time);
            return 2;
        }
        else
        {
            return 0;
        }
    }

}