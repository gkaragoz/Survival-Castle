using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor))]
public class CharacterAttacker : MonoBehaviour {

    public Action onAttacking;

    [Header("Initialization")]
    [SerializeField]
    private Projectile _arrowProjectile;
    [SerializeField]
    private Transform _projectileSpawnTransform;

    [Header("Settings")]
    [SerializeField]
    [Range(1, 89)]
    private float _shootAngle = 45f;
    [SerializeField]
    private float _attackRate = 1f;
    [SerializeField]
    private float _attackDamage = 50f;

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

        // Initialize projectile physics.
        Vector3 targetPosition = GameManager.instance.Target.position;
        Projectile projectile = Instantiate(_arrowProjectile, _projectileSpawnTransform.position, Quaternion.identity);
        Vector3 forceVector = HelperArcProjectile.MagicShoot(_shootAngle, targetPosition, _projectileSpawnTransform.position);

        // Set projectile damage.
        projectile.Damage = _attackDamage;

        // Force for apply to projectile.
        projectile.AddForce(forceVector);

        AudioManager.instance.Play("SfxXBowArrowRelease");
    }

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
