using System.Collections;
using System.Collections.Generic;
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

    private List<CharacterController> _enemies = new List<CharacterController>();
    public List<CharacterController> Enemies { get { return _enemies; } }
    public float Tickrate { get { return _tickRate; } }

    private Coroutine IControllingProcessCoroutine;

    private void StartAllMovements() {
        int enemiesCount = _enemies.Count;

        for (int ii = 0; ii < enemiesCount; ii++) {
            _enemies[ii].StartMoving();
        }
    }

    private void StopAllMovements() {
        int enemiesCount = _enemies.Count;

        for (int ii = 0; ii < enemiesCount; ii++) {
            _enemies[ii].StopMoving();
        }
    }

    private void StartAttacks() {
        int enemiesCount = _enemies.Count;

        for (int ii = 0; ii < enemiesCount; ii++) {
            _enemies[ii].StartAttacking();
        }
    }

    private void StopAllAttacks() {
        int enemiesCount = _enemies.Count;

        for (int ii = 0; ii < enemiesCount; ii++) {
            _enemies[ii].StopAttacking();
        }
    }

    private IEnumerator IControllingProcess() {
        StartAllMovements();

        while (_isRunning) {
            yield return new WaitForSeconds(_tickRate);

            for (int ii = 0; ii < _enemies.Count; ii++) {
                CharacterController agent = _enemies[ii];

                // If target has reached to his destination, then Stop Moving && Start Attacking.
                if (agent.HasReachedDestination) {
                    agent.StopMoving();
                    agent.StartAttacking();
                    continue;
                }

                // If target is dead, even he is reached to his destination, just stop moving!
                if (agent.IsDead) {
                    agent.StopMoving();

                    // In that situation, if it is attacking, stop it!
                    if (agent.IsAttacking) {
                        agent.StopAttacking();
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

    public void AddCharacter(CharacterController enemy) {
        _enemies.Add(enemy);
        enemy.onDead += RemoveCharacter;
    }

    public void RemoveCharacter(CharacterController enemy) {
        _enemies.Remove(enemy);
        enemy.onDead -= RemoveCharacter;

        Destroy(enemy.gameObject, 2f);
    }

    public void StartControl() {
        Debug.Log("Control starting.");

        _isRunning = true;
        IControllingProcessCoroutine = StartCoroutine(IControllingProcess());
    }

    public void StopControl() {
        Debug.Log("Control stopping.");

        _isRunning = false;
    }

}
