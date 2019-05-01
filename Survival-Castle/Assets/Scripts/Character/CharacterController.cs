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

    [SerializeField]
    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();
        _characterStats = GetComponent<Character>();

        _characterMotor.onStartMove += OnStartMove;
        _characterMotor.onStop += OnStop;
    }

    private void OnDestroy() {
        _characterMotor.onStartMove -= OnStartMove;
        _characterMotor.onStop -= OnStop;
    }

    private void Start() {
        _characterMotor.Move();
    }

    private void OnStartMove() {
    }

    private void OnStop() {
        _characterAttacker.Attack();
    }

    private void Die() {
        _isDead = true;

        onDead?.Invoke(this);
    }

    public void TakeDamage(float amount) {
        _characterStats.DecreaseHealth(amount);

        if (_characterStats.GetCurrentHealth() <= 0) {
            Die();
        }

        onTakeDamage?.Invoke();
    }

}
