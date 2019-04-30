using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private GameObject _target;

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _agent.SetDestination(_target.transform.position);
    }

    private void Update() {
        _animator.SetFloat("Velocity", _agent.velocity.magnitude);
    }

}
