using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    private Player player;
    public float lookRange;
    public LayerMask playerMask;

    public int life;

    bool isAttacking;
    public float attackSpeed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            target = PathFinding.findMaskObjectives(this.transform, lookRange, playerMask, target, null);
        } else {
            player = target.GetComponent<Player>();
            agent.SetDestination(target.position);
        }

        if (player != null) {
            if (Vector3.Distance(transform.position, target.position) < 1.5f) {
                if (!isAttacking) {
                    //play attack;
                    StartCoroutine(startAttack());
                }
            }
        }

        PathFinding.Dead(this.gameObject, life);
    }

    IEnumerator startAttack() {
        isAttacking = true;
        player.salubrity -= damage;
        print(player.name + ": " + player.salubrity);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
