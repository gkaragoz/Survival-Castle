using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _stoppingDistance = 3f;

    private Transform _target;

    private void Awake() {
        _target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    private void Update() {
        if (HasReachedDestination()) {
            Stop();
        } else {
            MoveToTarget();
        }
    }

    private void MoveToTarget() {

        Vector3 desiredRotation = _target.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredRotation), 0.1f);
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);

        _animator.SetFloat("Velocity", 1f);
    }

    private void Stop() {
        _animator.SetFloat("Velocity", 0f);
    }

    private bool HasReachedDestination() {
        return Vector3.Distance(transform.position, _target.position) <= _stoppingDistance ? true : false;
    }

}
