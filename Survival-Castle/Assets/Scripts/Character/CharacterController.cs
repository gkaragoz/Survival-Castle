using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterController : MonoBehaviour, IPooledObject {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;

    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;
    private Character _characterStats;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }
    public bool HasReachedDestination { get { return _characterMotor.HasReachedDestination; } }
    public bool IsAttacking { get { return _characterAttacker.IsAttacking; } }
    public bool IsMoving { get { return _characterMotor.IsMoving; } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();
        _characterStats = GetComponent<Character>();
    }

    private void Die() {
        _isDead = true;

        onDead?.Invoke(this);

        AudioManager.instance.Play("SfxEarnGolds");
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

        int randomFleshIndex = UnityEngine.Random.Range(1, 3);
        AudioManager.instance.Play("SfxImpactFlesh" + randomFleshIndex);

        onTakeDamage?.Invoke();
    }

    public void OnObjectReused() {

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
                    Destroy(other.gameObject, 1f);
                    break;
                case Projectile.OwnerEnum.Enemy:
                    break;
            }
        }
    }

}
