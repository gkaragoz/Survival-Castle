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
    [SerializeField]
    private BaseController _baseController;

    public Transform Target { get { return _target; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("BasicArcher");
        EnemyAIController.instance.Initialize();
        _baseController.StartSearchTarget();
    }

}
