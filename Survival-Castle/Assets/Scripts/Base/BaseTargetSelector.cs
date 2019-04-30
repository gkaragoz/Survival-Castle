using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseAttacker))]
public class BaseTargetSelector : MonoBehaviour {

    [SerializeField]
    private float _searchRate = 0.5f;
    [SerializeField]
    private CharacterController _selectedTarget;
    [SerializeField]
    private bool _isSearchingTarget = false;

    private BaseAttacker _baseAttacker;
    private Coroutine ISearchTargetCoroutine;

    public bool IsSearchingTarget { get { return _isSearchingTarget; } }
    public bool HasTarget { get { return _selectedTarget == null ? false : true; } }
    public CharacterController SelectedTarget { get { return _selectedTarget; } }

    private void Awake() {
        _baseAttacker = GetComponent<BaseAttacker>();
    }

    private IEnumerator ISearchTarget() {
        Debug.Log("Start searching target.");
        _isSearchingTarget = true;

        while (_isSearchingTarget) {
            yield return new WaitForSeconds(_searchRate);

            if (HasTarget) {
                if (_selectedTarget.IsDead == false) {
                    StopSearchTarget();
                    break;
                }
            }

            SelectClosestEnemy();

            if (HasTarget && !_selectedTarget.IsDead) {
                _baseAttacker.StartAttack();
            }
        }

        ISearchTargetCoroutine = null;

        yield return null;
    }

    private void SelectClosestEnemy() {
        int enemyCount = GameManager.instance.Enemies.Count;
        if (enemyCount <= 0) {
            return;
        }

        CharacterController closestTarget = GameManager.instance.Enemies[0];
        float distance = Vector3.Distance(transform.position, closestTarget.transform.position);

        for (int ii = 1; ii < enemyCount; ii++) {
            CharacterController potantialTarget = GameManager.instance.Enemies[ii];
            if (Vector3.Distance(transform.position, potantialTarget.transform.position) < distance) {
                closestTarget = potantialTarget;
            }
        }

        _selectedTarget = closestTarget;
    }

    public void StartSearchTarget() {
        if (ISearchTargetCoroutine == null) {
            ISearchTargetCoroutine = StartCoroutine(ISearchTarget());
        }
    }

    public void StopSearchTarget() {
        _isSearchingTarget = false;
        Debug.Log("Stop searching target.");
    }

}
