using UnityEngine;

public class InteractableTable:Interactable
{
    public GameObject sick;
    public bool isSick;
    public GameObject player;

    private float minWait = 6f;
    private float maxWait = 8f;
    
    //randomly makes tables sick and infect or not at the start of the game
    
    void Start()
    {
        isSick = Random.value >= 0.5;
        if (!isSick)
        {
            sick.SetActive(isSick);
            float time = Random.Range(minWait, maxWait);
            Invoke("DirtyTable", time);
        }
        else
        {
            this.DirtyTable();
        }
    }
    
    //checks to infect npcs if they are nearby
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

    //makes the table "dirty"  and sets up for if the user doesnt clean the table fast enough
    public void DirtyTable()
    {
        isSick = true;
        sick.SetActive(isSick);
		Invoke("LateDisinfect", 6);
    }
    
    //deducts points if the user doesnt disinfect the table in time
	private void LateDisinfect()
    {
        if (isSick)
        {
            Debug.Log("Table Not Clean");
            player.GetComponent<PlayerController>().playerScore -= 2;
            var temp = this.CleanTable();
        }
    }
    
    //awards points if the user cleans dirty tables
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