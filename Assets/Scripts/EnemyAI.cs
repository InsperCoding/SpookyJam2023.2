using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> waypoints;
    Transform currentTarget;
    private int index = 1;

    private NavMeshAgent agent;
    private Animator animator;
    private bool moving = true;
    private bool chasing = false;
    private bool patrolling = false;
    public GameObject player;


    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolling = true;

        if (waypoints.Count > 0 && waypoints[0] != null){
            currentTarget = waypoints[index];
            agent.SetDestination(currentTarget.position);
        }
    }

    void Update(){
        animator.SetBool("isMoving",moving);
        print(currentTarget);

        if (GetComponent<FieldOfView>().canSeePlayer && !chasing){
            GetComponent<FieldOfView>().angle=270;
            GetComponent<FieldOfView>().radius=80;
            patrolling = false;
            chasing=true;
            moving=true;
            currentTarget = player.transform;
            agent.SetDestination(currentTarget.position);
        } else if(!GetComponent<FieldOfView>().canSeePlayer && chasing){
            LostPlayer();
        }else if ((Vector3.Distance(transform.position, currentTarget.position) <= 2f) && moving && patrolling){
            GetComponent<FieldOfView>().radius=50;
            GetComponent<FieldOfView>().angle=60;
            chasing=false;
            MoveToNextWaypoint();
        }
       
    }

    void LostPlayer(){
        if (!GetComponent<FieldOfView>().canSeePlayer){
            chasing=false;
            patrolling=true;
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint(){
        patrolling=true;
        chasing = false;
        index++;
        if (index >= waypoints.Count){
            index = 0;
        }
        currentTarget = waypoints[index];
        agent.SetDestination(currentTarget.position);
        moving=true;
    }
}
