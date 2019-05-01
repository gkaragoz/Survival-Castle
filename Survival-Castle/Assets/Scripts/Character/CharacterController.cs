using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterController : MonoBehaviour {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;

    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;
    private Character _characterStats;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }
    public bool HasReachedDestination { get { return _characterMotor.HasReachedDestination; } }
    public bool IsAttacking { get { return _characterAttacker.IsAttacking; } }
    public bool IsMoving { get { return _characterMotor.IsMoving; } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();
        _characterStats = GetComponent<Character>();
    }

    private void Die() {
        _isDead = true;

        onDead?.Invoke(this);
    }

    public void StartMoving() {
        _characterMotor.StartMoving();
    }
    
    public void StopMoving() {
        _characterMotor.StopMoving();
    }

    public void StartAttacking() {
        _characterAttacker.StartAttacking();
    }

    public void StopAttacking() {
        _characterAttacker.StopAttacking();
    }

    public void TakeDamage(float amount) {
        _characterStats.DecreaseHealth(amount);

        if (_characterStats.GetCurrentHealth() <= 0) {
            Die();
        }

        onTakeDamage?.Invoke();
    }

}
