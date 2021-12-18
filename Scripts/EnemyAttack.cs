using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private NavMeshAgent Nav;
    private NavMeshHit hit;
    private bool blocked = false;
    private bool RunToPlayer = false;
    private float DistanceToPlayer;
    private bool IsChecking = true;
    private int FailedChecks = 0;
    public bool Crawler = false;
    public bool Ghoul = false;
    [SerializeField] Transform Player;
    [SerializeField] Animator Anim;
    [SerializeField] GameObject Enemy;
    [SerializeField] float MaxRange = 35.0f;
    [SerializeField] int MaxChecks = 3;
    [SerializeField] float ChaseSpeed = 8.5f;
    [SerializeField] float WalkSpeed = 1.6f;
    [SerializeField] float AttackDistance = 2.3f;
    [SerializeField] float AttackRotateSpeed = 2.0f;
    [SerializeField] float CheckTime = 3.0f;
    [SerializeField] GameObject ChaseMusic;
    [SerializeField] GameObject CrawlerAttackSound;
    [SerializeField] GameObject GhoulAttackSound;
    // Start is called before the first frame update
    void Start()
    {
        Nav = GetComponentInParent<NavMeshAgent>();
        Enemy.GetComponent<EnemyMove>().enabled = true;
        ChaseMusic.gameObject.SetActive(false);
         
    }

    // Update is called once per frame
    void Update()
    {
        
        DistanceToPlayer = Vector3.Distance(Player.position, Enemy.transform.position);
        if(DistanceToPlayer < MaxRange){
            if(IsChecking == true){
                IsChecking = false;

                blocked = NavMesh.Raycast(transform.position, Player.position, out hit, NavMesh.AllAreas);
                if(blocked == false){
                    Debug.Log("I can see the player");
                    RunToPlayer = true;
                }
                else{
                    Debug.Log("Where is the player?");
                    RunToPlayer = false;
                    Anim.SetInteger("State",0);
                    FailedChecks++;
                }
                StartCoroutine(TimedCheck());
            }
        }

        if(RunToPlayer && !Global.WinTheGame){
            Enemy.transform.rotation = Quaternion.LookRotation(Player.transform.position - Enemy.transform.position);
            
            Enemy.GetComponent<EnemyMove>().enabled = false;
            ChaseMusic.gameObject.SetActive(true);
            if(DistanceToPlayer > AttackDistance){
                Nav.isStopped = false;
                Anim.SetInteger("State",1);
                Nav.acceleration = 24;
                Nav.SetDestination(Player.position);
                Nav.speed = ChaseSpeed;
                CrawlerAttackSound.gameObject.SetActive(false);
                GhoulAttackSound.gameObject.SetActive(false);
            }
            else{
                Nav.isStopped = true;
                Debug.Log("I am attacking");
                Anim.SetInteger("State",2);
                if(Crawler)CrawlerAttackSound.gameObject.SetActive(true);
                if(Ghoul)GhoulAttackSound.gameObject.SetActive(true);
                Nav.acceleration = 180;
                if(Global.HP > 0){
                    if(Ghoul){
                        Global.HP -= 0.06f;
                    }
                    else{
                        Global.HP -= 0.03f;
                    }
                }
                    
            }
        }
        else if(!RunToPlayer){
            // Nav.isStopped = true;
            Anim.SetInteger("State",0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            RunToPlayer = true;
        }
    }
    IEnumerator TimedCheck(){
        yield return new WaitForSeconds(CheckTime);
        IsChecking = true;

        if(FailedChecks > MaxChecks || Global.WinTheGame){
            Enemy.GetComponent<EnemyMove>().enabled = true; 
            Nav.isStopped = false;
            Nav.speed = WalkSpeed;
            FailedChecks = 0;
            ChaseMusic.gameObject.SetActive(false);
        }
    }
}
