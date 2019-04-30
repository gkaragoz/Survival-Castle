using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private float _spawnRate = 2f;
    [SerializeField]
    private float radius = 10f;
    [SerializeField]
    private GameObject _archerPrefab;

    private void Start() {
        InvokeRepeating("Spawn", 1, _spawnRate);
    }

    private void Spawn() {
        GameObject newArcher = Instantiate(_archerPrefab, this.transform);
        newArcher.transform.position = RandomPointOnCircleEdge(radius);

        GameManager.instance.AddCharacter(newArcher);
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

}
