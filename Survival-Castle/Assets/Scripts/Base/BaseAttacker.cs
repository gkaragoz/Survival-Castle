using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseAttacker : MonoBehaviour {

    public Action onAttackStarted;
    public Action onAttackStopped;

    [Header("Initialization")]
    [SerializeField]
    private Projectile _arrowProjectile;

    [Header("Settings")]
    [SerializeField]
    private bool _isLocked = false;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _selectedTarget;

    private BaseTargetSelector _baseTargetSelector;
    private Base _baseStats;
    private Coroutine IAttackCoroutine;

    public bool IsAttacking { get { return _isAttacking; } }
    public float AttackRange { get { return _baseStats.GetAttackRange(); } }
    public float AttackRate { get { return _baseStats.GetAttackRange(); } }
    public float AttackDamage { get { return _baseStats.GetAttackDamage(); } }

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
        _baseStats = GetComponent<Base>();

        _baseTargetSelector.onTargetSelected += OnTargetSelected;
    }

    private void OnDestroy() {
        _baseTargetSelector.onTargetSelected -= OnTargetSelected;
    }

    private void OnTargetSelected(CharacterController selectedTarget) {
        this._selectedTarget = selectedTarget;

        StartAttack();
    }

    private IEnumerator IAttack() {
        //LogManager.instance.AddLog("[BASE] Start attacking to target.");
        _isAttacking = true;

        onAttackStarted?.Invoke();

        while (_isAttacking) {
            yield return new WaitForSeconds(_baseStats.GetAttackRate());
            if (_isLocked) {
                continue;
            }

            if (_selectedTarget == null) {
                StopAttack();
                break;
            }

            if (_selectedTarget.IsDead) {
                StopAttack();
                break;
            }

            if (IsTargetInRange()) {
                Attack();
                //_baseTargetSelector.SelectedTarget.TakeDamage(_attackDamage);
            }
        }

        IAttackCoroutine = null;

        yield return null;
    }

    private bool IsTargetInRange() {
        return Vector3.Distance(transform.position, _selectedTarget.transform.position) <= _baseStats.GetAttackRange() ? true : false;
    }

    private void Attack() {
        Vector3 targetPosition = _selectedTarget.transform.position;
        Projectile projectile = Instantiate(_arrowProjectile, transform.position, Quaternion.identity);
        Vector3 forceVector = HelperArcProjectile.MagicShoot(_baseStats.GetShootAngle(), targetPosition, transform.position);

        // Set projectile damage.
        projectile.Damage = AttackDamage;

        // Set projectile's owner.
        projectile.SetOwner(Projectile.OwnerEnum.Base);

        // Force for apply to projectile.
        projectile.AddForce(forceVector);
    }

    private void OnDrawGizmos() {
        if (_baseStats == null) {
            return;
        }

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.transform.position, _baseStats.GetAttackRange());
    }

    public void StartAttack() {
        if (IAttackCoroutine == null) {
            IAttackCoroutine = StartCoroutine(IAttack());
        }
    }

    public void StopAttack() {
        //LogManager.instance.AddLog("[BASE] Stop attacking.");
        _isAttacking = false;

        onAttackStopped?.Invoke();
    }

}
