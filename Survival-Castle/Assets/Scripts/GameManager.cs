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

    [Header("Initialization")]
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private BaseController _baseController;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController[] _enemies;
    public CharacterController[] Enemies { get { return _enemies; } }

    public Transform Target { get { return _target; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("BasicArcher");
        ObjectPooler.instance.InitializePool("Arrow");
        InitializeEnemies();
    }

    public void InitializeEnemies() {
        GameObject[] enemyObjs = ObjectPooler.instance.GetGameObjectsOnPool("BasicArcher");
        _enemies = new CharacterController[enemyObjs.Length];

        for (int ii = 0; ii < enemyObjs.Length; ii++) {
            _enemies[ii] = enemyObjs[ii].GetComponent<CharacterController>();
        }

        LogManager.instance.AddLog("[POOL ENEMIES]" + " has been initialized.");
    }

}
