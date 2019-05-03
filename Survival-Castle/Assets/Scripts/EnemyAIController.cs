using System.Collections;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {

    #region Singleton

    public static EnemyAIController instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Settings")]
    [SerializeField]
    private float _tickRate = 0.05f;
    [SerializeField]
    private bool _isRunning = false;

    public float Tickrate { get { return _tickRate; } }

    private Coroutine IControllingProcessCoroutine;

    private void StartAllMovements() {
        int enemiesCount = GameManager.instance.Enemies.Length;

        for (int ii = 0; ii < enemiesCount; ii++) {
            GameManager.instance.Enemies[ii].StartMoving();
        }
    }

    private void StopAllMovements() {
        int enemiesCount = GameManager.instance.Enemies.Length;

        for (int ii = 0; ii < enemiesCount; ii++) {
            GameManager.instance.Enemies[ii].StopMoving();
        }
    }

    private void StartAttacks() {
        int enemiesCount = GameManager.instance.Enemies.Length;

        for (int ii = 0; ii < enemiesCount; ii++) {
            GameManager.instance.Enemies[ii].StartAttacking();
        }
    }

    private void StopAllAttacks() {
        int enemiesCount = GameManager.instance.Enemies.Length;

        for (int ii = 0; ii < enemiesCount; ii++) {
            GameManager.instance.Enemies[ii].StopAttacking();
        }
    }

    private IEnumerator IControllingProcess() {
        while (_isRunning) {
            yield return new WaitForSeconds(_tickRate);

            for (int ii = 0; ii < GameManager.instance.Enemies.Length; ii++) {
                CharacterController agent = GameManager.instance.Enemies[ii];

                // If target is not active, skip it.
                if (!agent.gameObject.activeInHierarchy) {
                    continue;
                }

                // If target is dead, maybe we can skip it!
                if (agent.IsDead) {
                    // If it is moving, stop moving
                    if (agent.IsMoving) {
                        agent.StopMoving();
                    }

                    // If it is attacking, stop it!
                    if (agent.IsAttacking) {
                        agent.StopAttacking();
                    }
                    continue;
                }

                // If target has reached to his destination, then Stop Moving && Start Attacking.
                if (agent.HasReachedDestination) {
                    // If it is moving, stop moving. We're gonna fight.
                    if (agent.IsMoving) {
                        agent.StopMoving();
                    }

                    // If it is already attacking, just skip it.
                    if (!agent.IsAttacking) {
                        agent.StartAttacking();
                    }
                    continue;
                }

                // If target is not dead, has not reached to his destination, go, start movement!
                if (!agent.IsMoving) {
                    agent.StartMoving();
                    continue;
                }
            }
        }

        StopAllMovements();
        StopAllAttacks();

        yield break;
    }

    public void StartControl() {
        LogManager.instance.AddLog("[ENEMY AI] Control is starting.");

        _isRunning = true;
        IControllingProcessCoroutine = StartCoroutine(IControllingProcess());
    }

    public void StopControl() {
        LogManager.instance.AddLog("[ENEMY AI] Control is stopping.");

        _isRunning = false;
    }

}
