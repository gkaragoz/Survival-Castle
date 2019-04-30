using System;
using System.Collections;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public Action onStartMove;
    public Action onStop;
    public Action onDead;

    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _stoppingDistance = 3f;
    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isDead;

    private Transform _target;
    private Coroutine IMoveCoroutine;
    private Coroutine IDieCoroutine;

    public bool HasReachedDestination {
        get { return Vector3.Distance(transform.position, _target.position) <= _stoppingDistance ? true : false; }
    }

    public bool IsMoving {
        get { return _isMoving; }
    }

    public bool IsDead {
        get { return _isDead; }
    }

    private void Awake() {
        _target = GameManager.instance.Target;
    }

    private IEnumerator IMove() {
        _isMoving = true;

        onStartMove?.Invoke();

        while (_isMoving) {
            if (HasReachedDestination) {
                Stop();
                break;
            }

            LookToTarget();
            transform.Translate(Vector3.forward * _movementSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        IMoveCoroutine = null;

        yield return null;
    }

    private IEnumerator IDie() {
        yield return new WaitForSeconds(2f);
        _isDead = true;

        IDieCoroutine = null;

        onDead?.Invoke();

        yield return null;
    }

    private void LookToTarget() {
        Vector3 desiredRotation = _target.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredRotation), 0.1f);
    }

    public void Move() {
        if (IMoveCoroutine == null) {
            IMoveCoroutine = StartCoroutine(IMove());
        }
    }

    public void Die() {
        if (IDieCoroutine == null) {
            IDieCoroutine = StartCoroutine(IDie());
        }
    }

    public void Stop() {
        _isMoving = false;

        onStop?.Invoke();
    }

}
