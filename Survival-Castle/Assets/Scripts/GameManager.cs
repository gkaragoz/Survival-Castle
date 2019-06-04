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
    private Transform _target = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController[] _enemies = null;
    public CharacterController[] Enemies { get { return _enemies; } }

    public Transform Target { get { return _target; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("BasicArcher");
        ObjectPooler.instance.InitializePool("Arrow");
        ObjectPooler.instance.InitializePool("OverlayHealthBar");
        InitializeEnemies();
        InitializeOverlayHealthBars();

        EnemySpawner.instance.StartSpawning();
        BaseAIController.instance.StartControl();
        EnemyAIController.instance.StartControl();
    }

    public void InitializeEnemies() {
        GameObject[] enemyObjs = ObjectPooler.instance.GetGameObjectsOnPool("BasicArcher");
        _enemies = new CharacterController[enemyObjs.Length];

        for (int ii = 0; ii < enemyObjs.Length; ii++) {
            _enemies[ii] = enemyObjs[ii].GetComponent<CharacterController>();
        }

        Debug.Log("[POOL ENEMIES]" + " has been initialized.");
    }

    public void InitializeOverlayHealthBars() {
        GameObject[] overlayHealthBarObjs = ObjectPooler.instance.GetGameObjectsOnPool("OverlayHealthBar");

        for (int ii = 0; ii < overlayHealthBarObjs.Length; ii++) {
            CharacterController _characterController = _enemies[ii];
            overlayHealthBarObjs[ii].GetComponent<OverlayHealthBar>().Initialize(_characterController);
        }

        Debug.Log("[POOL OVERLAY HEALTH BARS]" + " have been initialized.");
    }

}
