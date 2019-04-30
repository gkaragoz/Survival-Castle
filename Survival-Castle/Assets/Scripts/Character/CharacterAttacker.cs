using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor))]
public class CharacterAttacker : MonoBehaviour {

    public Action onAttack;

    [SerializeField]
    private float _attackRate = 1f;
    [SerializeField]
    private bool _isAttacking;

    private CharacterController _characterController;
    private CharacterMotor _characterMotor;

    private Coroutine IAttackCoroutine;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
    }

    public bool IsAttacking {
        get { return _isAttacking; }
    }

    private IEnumerator IAttack() {
        _isAttacking = true;

        while (_isAttacking) {
            yield return new WaitForSeconds(1f);

            if (_characterController.IsDead) {
                Stop();
                break;
            }

            onAttack?.Invoke();
        }

        yield return null;
    }

    public void Attack() {
        if (IAttackCoroutine == null) {
            IAttackCoroutine = StartCoroutine(IAttack());
        }

        onAttack?.Invoke();
    }

    public void Stop() {
        _isAttacking = false;
    }

}
