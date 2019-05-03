using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseAttacker))]
public class BaseTargetSelector : MonoBehaviour {

    public Action onSearchTargetStarted;
    public Action onSearchTargetStopped;
    public Action<CharacterController> onTargetSelected;

    [Header("Settings")]
    [SerializeField]
    private bool _isLocked = false;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _selectedTarget;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isSearchingTarget = false;

    private BaseAttacker _baseAttacker;
    private Base _baseStats;
    private Coroutine ISearchTargetCoroutine;

    public bool IsSearchingTarget { get { return _isSearchingTarget; } }
    public bool HasTarget { get { return _selectedTarget == null ? false : true; } }
    public float SearchRate { get { return _baseStats.GetSearchRate(); } }

    private void Awake() {
        _baseAttacker = GetComponent<BaseAttacker>();
        _baseStats = GetComponent<Base>();

        _baseAttacker.onAttackStopped += OnAttackStopped;
    }

    private void OnDestroy() {
        _baseAttacker.onAttackStopped -= OnAttackStopped;
    }

    private void OnAttackStopped() {
        StartSearchTarget();
    }

    private IEnumerator ISearchTarget() {
        //LogManager.instance.AddLog("[BASE] Start searching target.");
        _selectedTarget = null;
        _isSearchingTarget = true;

        onSearchTargetStarted?.Invoke();

        while (_isSearchingTarget) {
            yield return new WaitForSeconds(_baseStats.GetSearchRate());

            if (_isLocked) {
                continue;
            }

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
        int enemyCount = EnemyAIController.instance.Enemies.Length;
        if (enemyCount <= 0) {
            return;
        }

        CharacterController closestTarget = EnemyAIController.instance.Enemies[0];
        float distance = Vector3.Distance(transform.position, closestTarget.transform.position);

        for (int ii = 0; ii < enemyCount; ii++) {
            CharacterController potantialTarget = EnemyAIController.instance.Enemies[ii];
            if (potantialTarget.IsDead) {
                continue;
            }

            if (!potantialTarget.gameObject.activeInHierarchy) {
                continue;
            }

            if (Vector3.Distance(transform.position, potantialTarget.transform.position) <= distance) {
                closestTarget = potantialTarget;
                _selectedTarget = closestTarget;

                onTargetSelected?.Invoke(_selectedTarget);
            }
        }
    }

    public void StartSearchTarget() {
        if (ISearchTargetCoroutine == null) {
            ISearchTargetCoroutine = StartCoroutine(ISearchTarget());
        }
    }

    public void StopSearchTarget() {
        _isSearchingTarget = false;
        //LogManager.instance.AddLog("[BASE] Stop searching target.");

        onSearchTargetStopped?.Invoke();
    }

}
