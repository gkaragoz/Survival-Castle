using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterController : MonoBehaviour, IPooledObject {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;
    public Action onReused;

    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;
    private Character _characterStats;

    [Header("Initializations")]
    [SerializeField]
    private SFXImpactFlesh _SFXImpactFlesh = null;
    [SerializeField]
    private SFXEarnGolds _SFXEarnGolds = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }
    public bool HasReachedDestination { get { return _characterMotor.HasReachedDestination; } }
    public bool IsAttacking { get { return _characterAttacker.IsAttacking; } }
    public bool IsMoving { get { return _characterMotor.IsMoving; } }
    public float CurrentHealth { get { return _characterStats.GetCurrentHealth(); } }
    public float MaxHealth { get { return _characterStats.GetMaxHealth(); } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();
        _characterStats = GetComponent<Character>();
    }

    private void Die() {
        _isDead = true;

        onDead?.Invoke(this);

        StartCoroutine(ISetDeactiveSelf());

        _SFXEarnGolds.Play();
    }

    public IEnumerator ISetDeactiveSelf() {
        yield return new WaitForSeconds(_characterStats.GetDeactivatorTime());
        this.gameObject.SetActive(false);

        yield break;
    }

    public void StartMoving() {
        _characterMotor.StartMoving();
    }
    
    public void StopMoving() {
        _characterMotor.StopMoving();
    }

    public void StartAttacking() {
        _characterAttacker.StartAttacking();
    }

    public void StopAttacking() {
        _characterAttacker.StopAttacking();
    }

    public void TakeDamage(float amount) {
        _characterStats.DecreaseHealth(amount);

        if (_characterStats.GetCurrentHealth() <= 0) {
            Die();
            return;
        }

        _SFXImpactFlesh.Play();

        onTakeDamage?.Invoke();
    }

    public void OnObjectReused() {
        _characterStats.SetCurrentHealth(_characterStats.GetMaxHealth());
        _isDead = false;

        onReused?.Invoke();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            if (this.IsDead) {
                return;
            }

            Projectile projectile = other.GetComponent<Projectile>();

            switch (projectile.Owner) {
                case Projectile.OwnerEnum.Base:
                    TakeDamage(projectile.Damage);
                    break;
                case Projectile.OwnerEnum.Enemy:
                    break;
            }
        }
    }

}
