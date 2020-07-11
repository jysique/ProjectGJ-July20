using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    NavMeshAgent agent;
    public float speed;
    public float lookRange;
    public Transform goal;
    public LayerMask enemyMask;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        if (target == null) {
            target = goal;
        }
        target = findMaskObjectives(this.transform, lookRange, enemyMask, target,goal);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            target = goal;
        } else {
            agent.SetDestination(target.position);
        }

        target = findMaskObjectives(this.transform, lookRange, enemyMask, target,goal);
        agent.SetDestination(target.position);

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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}
