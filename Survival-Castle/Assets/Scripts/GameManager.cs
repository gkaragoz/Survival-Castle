using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private Transform _target;

    private EnemySpawner _enemySpawner;

    public Transform Target { get { return _target; } }

    private void Start() {
        _enemySpawner = GetComponent<EnemySpawner>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _enemySpawner.StartSpawning();
        } 

        if (Input.GetKeyDown(KeyCode.Tab)) {
            _enemySpawner.StopSpawning();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            EnemyAIController.instance.StartControl();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            EnemyAIController.instance.StopControl();
        }
    }

}
