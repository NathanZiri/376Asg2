using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //parameter instantiation
    public CharacterController cc;
    public float radius = 6;
    public float speed = 10f;
    private float smoothTurn;
    public int playerScore = 0;
    public GameObject explosionEffects;
    bool modeType = false;
    public TextMeshProUGUI TextPro;
    public bool speedMod = true;
    public GameObject finalScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    //for visualizing and debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    //for setting if it is the easy or hard game mode
    public void ModifyMode()
    {
        modeType = true;
    }
    
    //resets user speed after using the boost in the harder game mode
    IEnumerator ResetSpeed()
    {
        speed = speed / 1.5f;
        yield return new WaitForSeconds(8);
        speedMod = true;
    }
    //when the player reaches a score of -20 the end screen is brought up
    private void EndScreen()
    {
        Time.timeScale = 0;
        finalScene.SetActive(true);
    }
    
    /*
     * checks score for ending the game
     * handles movement
     * checks for interacts between, a single or multiple npcs and tables
     */
    private void Update()
    {
        if (playerScore < -20)
        {
            this.EndScreen();
        }
        if (Input.GetButtonDown("SpeedUp") && speedMod && modeType)
        {
            speedMod = false;
            speed = speed*1.5f;
            Invoke("ResetSpeed", 7);
        }

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
        TextPro.text = playerScore.ToString();


        if(Input.GetButtonDown("Interact"))
        {
            int numHit = 0;
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius);
            foreach(var hitEnemy in hitEnemies){
                InteractableEnemy intr = hitEnemy.GetComponent<InteractableEnemy>();
                if (intr != null)
                {
                    numHit++;
                    playerScore += intr.IncreaseScore();
                    if (modeType)
                        playerScore++;
                }
            }

            
            if (numHit >= 2)
            {
                Collider[] moveEnemies = Physics.OverlapSphere(transform.position, radius);
                foreach(var movEnemy in moveEnemies){
                    EnemyMovement intr = movEnemy.GetComponent<EnemyMovement>();
                    if (intr != null)
                    {
                        intr.Relocate();
                        playerScore ++;
                    }
                }
                Instantiate(explosionEffects, transform.position, transform.rotation);
            }
            
            Collider[] hitTables = Physics.OverlapSphere(transform.position, radius);
            foreach(var hitTable in hitTables){
                InteractableTable intr = hitTable.GetComponent<InteractableTable>();
                if (intr != null && intr.isSick)
                {
                    playerScore += intr.CleanTable();
                    if (modeType)
                        playerScore++;
                }
            }
            
            
        }
    }

}
