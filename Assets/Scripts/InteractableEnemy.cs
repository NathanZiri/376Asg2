using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableEnemy : Interactable
{
    public string type;
    Material[] userType = new Material [4];


    private void Start()
    {
        string[] t = {"V", "U", "UM", "S", "I"};
        int val = Random.Range(0, 5);

        type = t[val];
    }    
    private void Update()
    {
        
        
        int percentageChance = Random.Range(0, 100);
        RaycastHit hit;
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 3);
        foreach(var hitEnemy in hitEnemies){
            InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
            if (intr != null)
            {
                if (intr.type.Equals("I"))
                {
                    if (type.Equals("U"))
                    {
                        modifyType(percentageChance < 1 ? false: true);
                    }else if (type.Equals("UM"))
                    {
                        modifyType(percentageChance < 2 ? false: true);
                    }else if (type.Equals("S"))
                    {
                        modifyType(percentageChance < 5 ? false: true);
                    }
                }
            }
        }
    }

    public void modifyType(bool alreadySick)
    {
        type = alreadySick ? "U" : "I";
    }

    private void changeMat()
    {
        if (type.Equals("V"))
        {
            
        }else if (type.Equals("U"))
        {
            
        }else if (type.Equals("UM"))
        {
            
        }else if (type.Equals("S"))
        {
            
        }else if (type.Equals("I"))
        {
            
        }
    }
    
    public int IncreaseScore()
    {
        return 5;
    }
    
}
