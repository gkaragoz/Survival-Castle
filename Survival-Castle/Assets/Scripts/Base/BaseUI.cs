using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BaseController), typeof(Base))]
public class BaseUI : Menu {

    [SerializeField]
    private Slider _slider;

    private BaseController _baseController;
    private Base _baseStats;

    private void Awake() {
        _baseController = GetComponent<BaseController>();
        _baseStats = GetComponent<Base>();

        _baseController.onTakeDamage += OnTakeDamage;
        _baseController.onDead += OnDead;
    }

    private void OnDestroy() {
        _baseController.onTakeDamage -= OnTakeDamage;
        _baseController.onDead -= OnDead;
    }

    private void Start() {
        _slider.maxValue = _baseStats.GetMaxHealth();
        _slider.value = _baseStats.GetCurrentHealth();
    }

    private void OnTakeDamage() {
        _slider.value = _baseStats.GetCurrentHealth();
    }

    private void OnDead() {
        Debug.Log("[BASE] has been destroyed!");
    }

}
