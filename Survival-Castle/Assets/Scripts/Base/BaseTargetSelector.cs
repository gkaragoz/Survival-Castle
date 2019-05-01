using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseAttacker))]
public class BaseTargetSelector : MonoBehaviour {

    public Action onSearchTargetStarted;
    public Action onSearchTargetStopped;
    public Action onTargetSelected;

    [Header("Settings")]
    [SerializeField]
    private float _searchRate = 0.5f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _selectedTarget;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isSearchingTarget = false;

    private BaseAttacker _baseAttacker;
    private Coroutine ISearchTargetCoroutine;

    public bool IsSearchingTarget { get { return _isSearchingTarget; } }
    public bool HasTarget { get { return _selectedTarget == null ? false : true; } }
    public CharacterController SelectedTarget { get { return _selectedTarget; } }
    public float SearchRate { get { return _searchRate; } }

    private void Awake() {
        _baseAttacker = GetComponent<BaseAttacker>();

        _baseAttacker.onAttackStopped += OnAttackStopped;
    }

    private void OnDestroy() {
        _baseAttacker.onAttackStopped -= OnAttackStopped;
    }

    private void OnAttackStopped() {
        StartSearchTarget();
    }

    private IEnumerator ISearchTarget() {
        //Debug.Log("Start searching target.");
        _selectedTarget = null;
        _isSearchingTarget = true;

        onSearchTargetStarted?.Invoke();

        while (_isSearchingTarget) {
            yield return new WaitForSeconds(_searchRate);

            if (!HasTarget) {
                SelectClosestTarget();
            }

            if (HasTarget) {
                StopSearchTarget();
                break;
            }
        }

        ISearchTargetCoroutine = null;

        yield return null;
    }

    private void SelectClosestTarget() {
        int enemyCount = EnemyAIController.instance.Enemies.Count;
        if (enemyCount <= 0) {
            return;
        }

        CharacterController closestTarget = EnemyAIController.instance.Enemies[0];
        float distance = Vector3.Distance(transform.position, closestTarget.transform.position);

        for (int ii = 1; ii < enemyCount; ii++) {
            CharacterController potantialTarget = EnemyAIController.instance.Enemies[ii];
            if (potantialTarget.IsDead) {
                continue;
            }

            if (Vector3.Distance(transform.position, potantialTarget.transform.position) < distance) {
                closestTarget = potantialTarget;
            }
        }

        _selectedTarget = closestTarget;

        onTargetSelected?.Invoke();
    }

    public void StartSearchTarget() {
        if (ISearchTargetCoroutine == null) {
            ISearchTargetCoroutine = StartCoroutine(ISearchTarget());
        }
    }

    public void StopSearchTarget() {
        _isSearchingTarget = false;
        //Debug.Log("Stop searching target.");

        onSearchTargetStopped?.Invoke();
    }

}
