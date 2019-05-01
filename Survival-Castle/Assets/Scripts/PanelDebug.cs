using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDebug : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtAITickrate;
    [SerializeField]
    private TextMeshProUGUI _txtBaseTargetSearcherRate;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackRate;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackDamage;
    [SerializeField]
    private TextMeshProUGUI _txtBaseAttackRange;

    [SerializeField]
    private Button _btnSpawner;
    [SerializeField]
    private TextMeshProUGUI _txtSpawner;
    [SerializeField]
    private Button _btnAIController;
    [SerializeField]
    private TextMeshProUGUI _txtAIController;
    [SerializeField]
    private Color _startColor;
    [SerializeField]
    private Color _stopColor;

    [SerializeField]
    private BaseController _baseController;

    private bool _isSpawnerStarted = false;
    private bool _isAIController = false;

    private void Update() {
        _txtAITickrate.text = "AI BRAIN TICK RATE: \t\t\t" + EnemyAIController.instance.Tickrate.ToString() + " (sec)";
        _txtBaseTargetSearcherRate.text = "BASE TARGET SEARCH RATE: \t" + _baseController.SearchRate.ToString() + " (sec)";
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

    public void OnClick_BtnAIController() {
        _isAIController = !_isAIController;

        if (_isAIController) {
            _txtAIController.text = "STOP";
            _btnAIController.GetComponent<Image>().color = _stopColor;
            EnemyAIController.instance.StartControl();
        } else {
            _txtAIController.text = "START";
            _btnAIController.GetComponent<Image>().color = _startColor;
            EnemyAIController.instance.StopControl();
        }
    }

}
