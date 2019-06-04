using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor))]
public class CharacterAttacker : MonoBehaviour {

    public Action onAttacking;

    [Header("Initialization")]
    [SerializeField]
    private Transform _projectileSpawnTransform = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _nextAttack = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;

    private CharacterController _characterController;
    private CharacterMotor _characterMotor;
    private Character _characterStats;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterStats = GetComponent<Character>();
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

        _nextAttack = Time.time + _characterStats.GetAttackRate();

        onAttacking?.Invoke();

        // Initialize projectile physics.
        Vector3 targetPosition = GameManager.instance.Target.position;
        Projectile projectile = ObjectPooler.instance.SpawnFromPool("Arrow", _projectileSpawnTransform.position, Quaternion.identity).GetComponent<Projectile>();
        Vector3 forceVector = HelperArcProjectile.MagicShoot(_characterStats.GetShootAngle(), targetPosition, _projectileSpawnTransform.position);

        // Set projectile damage.
        projectile.Damage = _characterStats.GetAttackDamage();

        // Set projectile's owner.
        projectile.SetOwner(Projectile.OwnerEnum.Enemy);

        // Force for apply to projectile.
        projectile.AddForce(forceVector);
    }

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
