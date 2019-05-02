using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseAttacker : MonoBehaviour {

    public Action onAttackStarted;
    public Action onAttackStopped;

    [Header("Initialization")]
    [SerializeField]
    private Rigidbody _projectileRB;
    [SerializeField]
    private ProjectileArcArray _projectileArcArray;

    [Header("Settings")]
    [SerializeField]
    private float _attackRange = 20f;
    [SerializeField]
    private float _attackRate = 0.5f;
    [SerializeField]
    private float _attackDamage = 50f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _selectedTarget;

    private BaseTargetSelector _baseTargetSelector;
    private Coroutine IAttackCoroutine;

    public bool IsAttacking { get { return _isAttacking; } }
    public float AttackRange { get { return _attackRange; } }
    public float AttackRate { get { return _attackRange; } }
    public float AttackDamage { get { return _attackDamage; } }

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();

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
            yield return new WaitForSeconds(_attackRate);
            if (_selectedTarget == null) {
                StopAttack();
                break;
            }

            if (!_selectedTarget.gameObject.activeInHierarchy) {
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
        return Vector3.Distance(transform.position, _selectedTarget.transform.position) <= _attackRange ? true : false;
    }

    private void Attack() {
        Vector3 targetPosition = _selectedTarget.transform.position;
        Rigidbody projectile = Instantiate(_projectileRB, _projectileArcArray.transform.position, Quaternion.identity);
        Vector3 forceVector = _projectileArcArray.MagicShoot(targetPosition);

        // Draw preposition of launch arc.
        RenderArc(targetPosition);

        // Force for apply to projectile.
        projectile.AddForce(forceVector, ForceMode.VelocityChange);
    }

    private void RenderArc(Vector3 targetPosition) {
        //Set visible of launchArcRenderer.
        _projectileArcArray.SetVisibility(true);

        //Keep calculate and render the launchArc.
        _projectileArcArray.SetArcPoints(targetPosition);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.transform.position, _attackRange);
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
