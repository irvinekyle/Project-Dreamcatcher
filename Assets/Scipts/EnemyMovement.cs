using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    Transform player;               // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float distToPlayer;       //stores the distance to player


    void Awake() {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Update() {
        // If the enemy and the player have health left...
        
        if (enemyHealth.currentHealth > 0) {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        // Otherwise...
        else {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
        nav.SetDestination(player.position);
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distToPlayer <= 2.0 && enemyHealth.currentHealth > 0) {
            GetComponent<Animator>().Play("attack_short_001");
        }
        else if (distToPlayer >= 30.0 && enemyHealth.currentHealth > 0) {
            GetComponent<Animator>().Play("move_forward_fast");
            nav.speed = nav.speed + 3.0f;
        }
        else if(distToPlayer >= 5.0 && enemyHealth.currentHealth > 0) {
            GetComponent<Animator>().Play("move_forward");
            nav.speed = 3.5f;
        }
    }
}
