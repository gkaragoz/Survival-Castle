using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDebug : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtEnemiesAITickrate = null;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAITickrate = null;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackRate = null;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackDamage = null;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackRange = null;

    [SerializeField]
    private Button _btnSpawner = null;
    [SerializeField]
    private TextMeshProUGUI _txtSpawner = null;
    [SerializeField]
    private Button _btnEnemiesAIController = null;
    [SerializeField]
    private TextMeshProUGUI _txtEnemiesAIController = null;
    [SerializeField]
    private Button _btnBaseAIController = null;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAIController = null;
    [SerializeField]
    private Color _startColor = Color.black;
    [SerializeField]
    private Color _stopColor = Color.black;

    [SerializeField]
    private BaseController _baseController = null;

    private bool _isSpawnerStarted = false;
    private bool _isEnemiesAIController = false;
    private bool _isBaseAIController = false;

    private void Update() {
        _txtEnemiesAITickrate.text = "AI BRAIN TICK RATE: \t\t\t" + EnemyAIController.instance.Tickrate.ToString() + " (sec)";
        _txtBaseAITickrate.text = "BASE TARGET SEARCH RATE: \t" + BaseAIController.instance.Tickrate.ToString() + " (sec)";
        _txtBaseAttackRate.text = "BASE ATTACK RATE: \t\t\t" + _baseController.AttackRate.ToString() + " (sec)";
        _txtBaseAttackDamage.text = "BASE ATTACK DAMAGE: \t\t" + _baseController.AttackDamage.ToString();
        _txtBaseAttackRange.text = "BASE ATTACK RANGE: \t\t\t" + _baseController.AttackRange.ToString() + " (m)";
    }

    public void OnClick_BtnSpawner() {
        _isSpawnerStarted = !_isSpawnerStarted;

        if (_isSpawnerStarted) {
            _txtSpawner.text = "STOP";
            _btnSpawner.GetComponent<Image>().color = _stopColor; 
            EnemySpawner.instance.StartSpawning();
        } else {
            _txtSpawner.text = "START";
            _btnSpawner.GetComponent<Image>().color = _startColor; 
            EnemySpawner.instance.StopSpawning();
        }
    }

    public void OnClick_BtnEnemiesAIController() {
        _isEnemiesAIController = !_isEnemiesAIController;

        if (_isEnemiesAIController) {
            _txtEnemiesAIController.text = "STOP";
            _btnEnemiesAIController.GetComponent<Image>().color = _stopColor;
            EnemyAIController.instance.StartControl();
        } else {
            _txtEnemiesAIController.text = "START";
            _btnEnemiesAIController.GetComponent<Image>().color = _startColor;
            EnemyAIController.instance.StopControl();
        }
    }

    public void OnClick_BtnBaseAIController() {
        _isBaseAIController = !_isBaseAIController;

        if (_isBaseAIController) {
            _txtBaseAIController.text = "STOP";
            _btnBaseAIController.GetComponent<Image>().color = _stopColor;
            BaseAIController.instance.StartControl();
        } else {
            _txtBaseAIController.text = "START";
            _btnBaseAIController.GetComponent<Image>().color = _startColor;
            BaseAIController.instance.StopControl();
        }
    }

}
