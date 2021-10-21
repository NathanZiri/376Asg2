using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public float speed = 10f;
    private float smoothTurn;
    public int playerScore = 0; 

    // Start is called before the first frame update
    void Start()
    {
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
    
    // Update is called once per frame
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;

        if(dir.magnitude >= 0.1f)
        {
            float lookTo = Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookTo, ref smoothTurn, 0.15f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            cc.Move(dir*speed*Time.deltaTime);
        }


        if(Input.GetButtonDown("Interact"))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 3);
            foreach(var hitEnemy in hitEnemies){
                InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
                if (intr != null)
                {
                    playerScore += intr.IncreaseScore();
                }
            } 
            
            Collider[] hitTables = Physics.OverlapSphere(transform.position, 3);
            foreach(var hitTable in hitTables){
                InteractableTable intr = hitTable.GetComponent<InteractableTable>();
                if (intr != null)
                {
                    playerScore += intr.CleanTable();
                }
            }
            
            
        }
        Debug.Log(playerScore);
    }

}
