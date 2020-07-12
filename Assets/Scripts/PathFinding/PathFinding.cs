using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    GameController gController;
    NavMeshAgent agent;
    Player player;
    public float speed;
    public float lookEnemyRange;
    public float lookItemRange;
    public Transform goal;
    Transform item;
    public LayerMask itemMask;
    Transform enemy;
    public LayerMask enemyMask;
    
    public Transform actTarget;

    [Header ("Combat Stats")]
    public bool isAttacking;
    public int damage;
    public float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gController = FindObjectOfType<GameController>();
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
        goal = gController.goal;
        agent.speed = speed;
        if (actTarget == null) {
            actTarget = goal;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (actTarget == null) {
            actTarget = goal;
        }

        item = findMaskObjectives(this.transform, lookItemRange, itemMask, item, null);
        if (item != null) {
            actTarget = item;
        }
        enemy = findMaskObjectives(this.transform, lookEnemyRange, enemyMask, enemy, null);
        if (enemy != null) {
            actTarget = enemy;
        }

        agent.SetDestination(actTarget.position);

        if (enemy != null) {
            if (Vector3.Distance(transform.position, enemy.position) < 1.5f) {
                if (!isAttacking) {
                    //play attack
                    StartCoroutine(startAttack());
                }
            }
        }

        Dead(this.gameObject, player.salubrity);
    }

    IEnumerator startAttack() {
        isAttacking = true;
        EnemyController _enemy = enemy.GetComponent<EnemyController>();
        _enemy.life -= damage;
        print(_enemy.name + ": " + _enemy.life);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    public static Transform findMaskObjectives(Transform _Caller, float _size, LayerMask _mask, Transform _target, Transform defaultDestination) {
        Collider[] objectives = Physics.OverlapSphere(_Caller.position, _size, _mask);
        if (objectives.Length > 0) {
            foreach (Collider objective in objectives) {
                if (_target == null) {
                    _target = objective.transform;
                } else {
                    float distance = Vector3.Distance(_Caller.position, _target.position);
                    if (distance > Vector3.Distance(_Caller.position, objective.transform.position)) {
                        _target = objective.transform;
                        //Debug.Log(this.gameObject.name +"/ target: " + target.name);
                    }
                }
            }
        } else {
            _target = defaultDestination;
        }
        return _target;
    }

    public static void Dead(GameObject _Caller , int _life) {
        if (_life <= 0) {
            Destroy(_Caller);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookEnemyRange);
    }
}
