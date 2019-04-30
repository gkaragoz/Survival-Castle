using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private float _spawnRate = 2f;
    [SerializeField]
    private float radius = 10f;
    [SerializeField]
    private GameObject _archerPrefab;

    private Transform _target;

    private void Awake() {
        _target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    private void Start() {
        InvokeRepeating("Spawn", 1, _spawnRate);
    }

    private void Spawn() {
        GameObject newArcher = Instantiate(_archerPrefab, this.transform);
        newArcher.transform.position = RandomPointOnCircleEdge(radius);
    }

    private Vector3 RandomPointOnCircleEdge(float radius) {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 pointOnEdge = myPosition + (Random.insideUnitCircle.normalized * radius);
        return new Vector3(pointOnEdge.x, 0, pointOnEdge.y);
    }

    private void OnDrawGizmos() {
        if (_target == null) {
            _target = GameObject.FindGameObjectWithTag("Target").transform;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_target.transform.position, radius);
    }

}
