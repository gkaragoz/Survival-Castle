using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseAttacker : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Projectile _arrowProjectile;

    [Header("Debug")]
    [SerializeField]
    //[Utils.ReadOnly]
    private bool _isAttacking = false;
    [SerializeField]
    [Utils.ReadOnly]
    private float _nextAttack = 0;

    private BaseTargetSelector _baseTargetSelector;
    private Base _baseStats;

    public bool IsAttacking { get { return _isAttacking; } }
    public float AttackRange { get { return _baseStats.GetAttackRange(); } }
    public float AttackRate { get { return _baseStats.GetAttackRange(); } }
    public float AttackDamage { get { return _baseStats.GetAttackDamage(); } }

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
        _baseStats = GetComponent<Base>();
    }

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

        LaunchProjectile();
    }

    private bool IsTargetInRange() {
        return Vector3.Distance(transform.position, _baseTargetSelector.SelectedTarget.transform.position) <= _baseStats.GetAttackRange() ? true : false;
    }

    private void LaunchProjectile() {
        Vector3 targetPosition = _baseTargetSelector.SelectedTarget.transform.position;
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

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
