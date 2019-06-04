using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    public Action onDead;
    public Action onTakeDamage;
    public Action onCrewAdded;

    [Header("Settings")]
    [SerializeField]
    private bool _isAttacking = false;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isDead = false;
    [SerializeField]
    [Utils.ReadOnly]
    private Base _baseStats;

    private BaseTargetSelector _baseTargetSelector;
    private BaseAttacker _baseAttacker;
    private List<BaseCrew> _baseCrews = new List<BaseCrew>();

    public bool IsAttacking { get { return _isAttacking; } }
    public float AttackRange { get { return _baseAttacker.AttackRange; } }
    public float AttackRate { get { return _baseAttacker.AttackRate; } }
    public float AttackDamage { get { return _baseAttacker.AttackDamage; } }
    public bool HasTarget { get { return _baseTargetSelector.HasTarget; } }
    public CharacterController SelectedTarget { get { return _baseTargetSelector.SelectedTarget; } }
    public bool IsDead { get { return _isDead; } }

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

    public void AddCrew(bool isInitialization = false) {
        GameObject newCrewObj = new GameObject("Crew (" + (_baseCrews.Count + 1) + ")");

        BaseCrew newCrew = newCrewObj.AddComponent<BaseCrew>();
        newCrew.Initialize(_baseTargetSelector, _baseStats);

        _baseCrews.Add(newCrew);

        if (!isInitialization) {
            onCrewAdded?.Invoke();
        }
    }

    public void Attack(Vector3 targetPosition) {
        _baseAttacker.Attack(targetPosition);
    }

    public void StartAttacking() {
        for (int ii = 0; ii < _baseCrews.Count; ii++) {
            _baseCrews[ii].StartAttacking();
        }
    }

    public void StopAttacking() {
        for (int ii = 0; ii < _baseCrews.Count; ii++) {
            _baseCrews[ii].StopAttacking();
        }
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
