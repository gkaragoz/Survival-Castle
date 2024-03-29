﻿using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    #region Singleton

    public static EnemySpawner instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initializations")]
    [SerializeField]
    private string BASIC_ARCHER = "BasicArcher";

    [Header("Settings")]
    [SerializeField]
    private float _spawnRate = 2f;
    [SerializeField]
    private float radius = 10f;

    private void Spawn() {
        GameObject newArcher = ObjectPooler.instance.SpawnFromPool(BASIC_ARCHER, RandomPointOnCircleEdge(radius), Quaternion.identity);

        Debug.Log("[SPAWNER] " + newArcher.name + " has been spawned.");
    }

    private Vector3 RandomPointOnCircleEdge(float radius) {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 pointOnEdge = myPosition + (Random.insideUnitCircle.normalized * radius);
        return new Vector3(pointOnEdge.x, 0, pointOnEdge.y);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.transform.position, radius);
    }

    public void StartSpawning() {
        if (IsInvoking("Spawn")) {
            return;
        }

        Debug.Log("[SPAWNER] Set to on.");
        InvokeRepeating("Spawn", 1, _spawnRate);
    }

    public void StopSpawning() {
        Debug.Log("[SPAWNER] Set to off.");

        CancelInvoke("Spawn");
    }

}
