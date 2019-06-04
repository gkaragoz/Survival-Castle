using System.Collections;
using UnityEngine;

public class BaseCrew : BaseAttacker {

    [Header("Settings")]
    [SerializeField]
    private float _startToAttackDelayRandomizer = 0f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;

    public bool IsAttacking { get { return _isAttacking; } }

    private void Update() {
        if (_isAttacking) {
            Attack();
        }
    }

    private void Attack() {
        _isAttacking = true;

        // Wait cooldown for next attack.
        if (Time.time <= _nextAttack) {
            return;
        }

        // SelectedTarget is dead because of another projectile.
        if (_baseTargetSelector.SelectedTarget == null) {
            return;
        }

        _nextAttack = Time.time + _baseStats.GetManualAttackRate();

        LaunchProjectile(_baseTargetSelector.SelectedTarget.transform.position);
    }

    // Delay on start first attack.
    private IEnumerator IStartAttacking() {
        yield return new WaitForSeconds(Random.Range(0, _startToAttackDelayRandomizer));

        _isAttacking = true;
    }

    public void Initialize(BaseTargetSelector baseTargetSelector, Base baseStats, float startToAttackDelayRandomizer = 1f) {
        this._baseTargetSelector = baseTargetSelector;
        this._baseStats = baseStats;
        this._startToAttackDelayRandomizer = startToAttackDelayRandomizer;
    }

    public void StartAttacking() {
        StartCoroutine(IStartAttacking());
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
