using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseAttacker : MonoBehaviour {

    protected float _nextAttack = 0;

    protected BaseTargetSelector _baseTargetSelector;
    protected Base _baseStats;

    public float AttackRange { get { return _baseStats.GetAttackRange(); } }
    public float AttackRate { get { return _baseStats.GetAttackRate(); } }
    public float AttackDamage { get { return _baseStats.GetAttackDamage(); } }

    private void Awake() {
        if (_baseTargetSelector == null) {
            _baseTargetSelector = GetComponent<BaseTargetSelector>();
        } 
        if (_baseStats == null) {
            _baseStats = GetComponent<Base>();
        }
    }

    protected void LaunchProjectile(Vector3 targetPosition) {
        Projectile projectile = ObjectPooler.instance.SpawnFromPool("Arrow", transform.position, Quaternion.identity).GetComponent<Projectile>();
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

    public void Attack(Vector3 targetPosition) {
        if (Time.time <= _nextAttack) {
            return;
        }

        _nextAttack = Time.time + _baseStats.GetAttackRate();

        LaunchProjectile(targetPosition);
    }

}
