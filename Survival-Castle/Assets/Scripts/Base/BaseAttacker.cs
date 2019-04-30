using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseAttacker : MonoBehaviour {

    [SerializeField]
    private float _attackRange = 20f;
    [SerializeField]
    private float _attackRate = 0.5f;
    [SerializeField]
    private float _attackDamage = 50f;
    [SerializeField]
    private bool _isAttacking = false;

    private BaseTargetSelector _baseTargetSelector;
    private Coroutine IAttackCoroutine;

    public bool IsAttacking { get { return _isAttacking; } }

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
    }

    private IEnumerator IAttack() {
        Debug.Log("Start attacking to target.");
        _isAttacking = true;

        while (_isAttacking) {
            yield return new WaitForSeconds(_attackRate);

            if (!_baseTargetSelector.HasTarget) {
                break;
            }
            
            if (_baseTargetSelector.SelectedTarget.IsDead) {
                StopAttack();
                _baseTargetSelector.StartSearchTarget();
                break;
            }

            if (IsTargetInRange()) {
                Debug.Log("Attacking to: " + _baseTargetSelector.SelectedTarget);
                _baseTargetSelector.SelectedTarget.TakeDamage(_attackDamage);
            }
        }

        IAttackCoroutine = null;

        yield return null;
    }

    private bool IsTargetInRange() {
        return Vector3.Distance(transform.position, _baseTargetSelector.SelectedTarget.transform.position) <= _attackRange ? true : false;
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
        Debug.Log("Stop attacking.");
        _isAttacking = false;
    }

}
