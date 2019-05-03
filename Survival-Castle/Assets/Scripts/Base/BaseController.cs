using System;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    public Action onDead;
    public Action onTakeDamage;

    private BaseTargetSelector _baseTargetSelector;
    private BaseAttacker _baseAttacker;
    private Base _baseStats;

    public bool IsAttacking { get { return _baseAttacker.IsAttacking; } }
    public float AttackRange { get { return _baseAttacker.AttackRange; } }
    public float AttackRate { get { return _baseAttacker.AttackRate; } }
    public float AttackDamage { get { return _baseAttacker.AttackDamage; } }
    public bool HasTarget { get { return _baseTargetSelector.HasTarget; } }
    public CharacterController SelectedTarget { get { return _baseTargetSelector.SelectedTarget; } }
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

    public void StartAttacking() {
        _baseAttacker.StartAttacking();
    }

    public void StopAttacking() {
        _baseAttacker.StopAttacking();
    }

    public void SearchTarget() {
        _baseTargetSelector.SearchTarget();
    }

    public void DeselectTarget() {
        _baseTargetSelector.DeselectTarget();
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
