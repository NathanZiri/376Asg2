using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableEnemy : Interactable
{
    public string type;
    public bool masked;
    Material[] userType = new Material [4];
    public GameObject mask;
    public GameObject vaxSymbol;
    public GameObject susceptibleSymbol;
    public GameObject infectedSymbol;
    
    private void Start()
    {
        string[] t = {"V", "U", "S", "I"};
        int val = Random.Range(0, 4);
        masked = Random.Range(0, 2) == 1;
        type = t[val];
    }    
    private void Update()
    {
        if (type.Equals("V")) {
            vaxSymbol.SetActive(true);
            susceptibleSymbol.SetActive(false);
            infectedSymbol.SetActive(false);
        }
        else if (type.Equals("I")) {
            vaxSymbol.SetActive(false);
            susceptibleSymbol.SetActive(false);
            infectedSymbol.SetActive(true);
        }
        else if (type.Equals("U")) {
            vaxSymbol.SetActive(false);
            susceptibleSymbol.SetActive(false);
            infectedSymbol.SetActive(false);
        }
        else if (type.Equals("S")) {
            vaxSymbol.SetActive(false);
            susceptibleSymbol.SetActive(true);
            infectedSymbol.SetActive(false); 
        }
        mask.SetActive(masked);
        
        int percentageChance = Random.Range(0, 100);
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius);
        foreach(var hitEnemy in hitEnemies){
            InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
            if (intr != null)
            {
                if (intr.type.Equals("I"))
                {
                    if (type.Equals("U"))
                    {
                        modifyType(percentageChance < 1 ? false: true);
                    }else if (type.Equals("U") && masked)
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
        if (type.Equals("V"))
            return;
        type = alreadySick ? "U" : "I";
    }

    public int IncreaseScore()
    {
        int score = 0;
        
        if (!masked)
        {
            masked = true;
            score += 2;
        }
        
        if(type.Equals("S") || type.Equals("V"))
        {
            return score;
        }
        
        if(type.Equals("I"))
        {
            type = "U";
            score += 2;
        }
        
        return score;
    }
    
}
