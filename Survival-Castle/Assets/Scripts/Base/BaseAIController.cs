using System.Collections;
using UnityEngine;

public class BaseAIController : MonoBehaviour {

    #region Singleton

    public static BaseAIController instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initializations")]
    [SerializeField]
    private BaseController _baseController = null;

    [Header("Settings")]
    [SerializeField]
    private float _tickRate = 0.05f;
    [SerializeField]
    private bool _isRunning = false;

    private Coroutine IControllingProcessCoroutine;

    public float Tickrate { get { return _tickRate; } }

    private IEnumerator IControllingProcess() {
        while (_isRunning) {
            yield return new WaitForSeconds(_tickRate);

            // If base has target, then first search a target.
            // Target searcher knows that, skip the not active targets or skip the dead ones.
            if (!_baseController.HasTarget) {
                _baseController.SearchTarget();
            }

            // If we found a target, then check target status.
            if (_baseController.HasTarget) {

                // If selected target is dead, then check attacking status!
                if (_baseController.SelectedTarget.IsDead) {
                    
                    // If base is attacking, then stop it!
                    if (_baseController.IsAttacking) {
                        _baseController.StopAttacking();
                    }

                    // Let target selector know that target has been died!
                    _baseController.DeselectTarget();
                    continue;
                } 

                // If selected target is not dead, then check attacking status!
                if (_baseController.IsAttacking) {
                    continue;
                }

                // Every status has been checked, seriously, start attacking!
                _baseController.StartAttacking();
                continue;
            }

            // Every status has been checked, there is no target at this cycle. Stop attacking!
            _baseController.StopAttacking();
        }

        yield break;
    }

    public void StartControl() {
        Debug.Log("[BASE AI] Control is starting.");

        _isRunning = true;
        IControllingProcessCoroutine = StartCoroutine(IControllingProcess());
    }

    public void StopControl() {
        Debug.Log("[BASE AI] Control is stopping.");

        _isRunning = false;
    }

}
