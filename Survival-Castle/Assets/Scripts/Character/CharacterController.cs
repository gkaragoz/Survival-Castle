using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterController : MonoBehaviour {

    public Action<CharacterController> onDead;

    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private Slider _slider;

    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;

    [SerializeField]
    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();

        _characterMotor.onStartMove += OnStartMove;
        _characterMotor.onStop += OnStop;

        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
    }

    private void Update() {
        _slider.value = _currentHealth;
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
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            Die();
        }
    }

}
