using UnityEngine;

public class BaseCrew : BaseAttacker {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;
    [SerializeField]
    [Utils.ReadOnly]
    private float _nextAttack = 0;

    public bool IsAttacking { get { return _isAttacking; } }

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

        _nextAttack = Time.time + _baseStats.GetAttackRate();

        LaunchProjectile(_baseTargetSelector.SelectedTarget.transform.position);
    }

    public void Initialize(BaseTargetSelector baseTargetSelector, Base baseStats) {
        this._baseTargetSelector = baseTargetSelector;
        this._baseStats = baseStats;
    }

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
