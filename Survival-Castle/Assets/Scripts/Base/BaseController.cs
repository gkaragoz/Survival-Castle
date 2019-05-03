using System;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    public Action onDead;
    public Action onTakeDamage;

    private BaseTargetSelector _baseTargetSelector;
    private BaseAttacker _baseAttacker;
    private Base _baseStats;

    public bool IsSearchingTarget { get { return _baseTargetSelector.IsSearchingTarget; } }
    public bool IsAttacking { get { return _baseAttacker.IsAttacking; } }
    public float AttackRange { get { return _baseAttacker.AttackRange; } }
    public float AttackRate { get { return _baseAttacker.AttackRange; } }
    public float AttackDamage { get { return _baseAttacker.AttackDamage; } }
    public bool HasTarget { get { return _baseTargetSelector.HasTarget; } }
    public float SearchRate { get { return _baseTargetSelector.SearchRate; } }
    public bool IsDead { get { return _isDead; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isDead = false;

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
        _baseAttacker = GetComponent<BaseAttacker>();
        _baseStats = GetComponent<Base>();
    }

    private void Die() {
        _isDead = true;

        onDead?.Invoke();

        //_SFXEarnGolds.Play();
    }

    public void StartSearchTarget() {
        _baseTargetSelector.StartSearchTarget();
    }

    public void StopSearchTarget() {
        _baseTargetSelector.StopSearchTarget();
    }

    public void TakeDamage(float amount) {
        _baseStats.DecreaseHealth(amount);

        if (_baseStats.GetCurrentHealth() <= 0) {
            Die();
            return;
        }

        //_SFXImpactFlesh.Play();

        onTakeDamage?.Invoke();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            if (this.IsDead) {
                return;
            }

            Projectile projectile = other.GetComponent<Projectile>();

            switch (projectile.Owner) {
                case Projectile.OwnerEnum.Base:
                    break;
                case Projectile.OwnerEnum.Enemy:
                    TakeDamage(projectile.Damage);
                    break;
            }
        }
    }

}
