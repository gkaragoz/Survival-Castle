using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor))]
public class CharacterAttacker : MonoBehaviour {

    public Action onAttacking;

    [Header("Settings")]
    [SerializeField]
    private float _attackRate = 1f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _nextAttack = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking;

    private CharacterController _characterController;
    private CharacterMotor _characterMotor;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
    }

    public bool IsAttacking {
        get { return _isAttacking; }
    }

    private void Update() {
        if (_isAttacking) {
            Attack();
        }
    }

    private void Attack() {
        _isAttacking = true;

        if (Time.time <= _nextAttack) {
            return;
        }

        _nextAttack = Time.time + _attackRate;

        onAttacking?.Invoke();
    }

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
