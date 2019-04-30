using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterAttacker : MonoBehaviour {

    public Action onAttack;

    [SerializeField]
    private float _attackRate = 1f;
    [SerializeField]
    private bool _isAttacking;

    private CharacterMotor _characterMotor;

    private Coroutine IAttackCoroutine;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
    }

    public bool IsAttacking {
        get { return _isAttacking; }
    }

    private IEnumerator IAttack() {
        _isAttacking = true;

        while (_isAttacking) {
            yield return new WaitForSeconds(1f);

            if (_characterMotor.IsDead) {
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
