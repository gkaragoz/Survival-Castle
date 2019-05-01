using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour {

    public Action onStartMove;
    public Action onStopMove;

    [Header("Settings")]
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _stoppingDistance = 3f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isMoving = false;

    private Transform _target;
    private CharacterController _characterController;

    public bool HasReachedDestination {
        get { return Vector3.Distance(transform.position, _target.position) <= _stoppingDistance ? true : false; }
    }

    public bool IsMoving {
        get { return _isMoving; }
    }

    private void Awake() {
        _target = GameManager.instance.Target;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        if (_isMoving) {
            Move();
        }
    }

    private void Move() {
        _isMoving = true;

        LookToTarget();
        transform.Translate(Vector3.forward * _movementSpeed * Time.fixedDeltaTime);
    }

    private void LookToTarget() {
        Vector3 desiredRotation = _target.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredRotation), 0.1f);
    }

    public void StartMoving() {
        _isMoving = true;

        onStartMove?.Invoke();
    }

    public void StopMoving() {
        _isMoving = false;

        onStopMove?.Invoke();
    }

}
