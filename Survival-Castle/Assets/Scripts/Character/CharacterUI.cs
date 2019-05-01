using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(Character))]
public class CharacterUI : MonoBehaviour {

    [SerializeField]
    private Slider _slider;

    private CharacterController _characterController;
    private Character _characterStats;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterStats = GetComponent<Character>();

        _characterController.onTakeDamage += OnTakeDamage;
    }

    private void OnDestroy() {
        _characterController.onTakeDamage -= OnTakeDamage;
    }

    private void Start() {
        _slider.maxValue = _characterStats.GetMaxHealth();
        _slider.value = _characterStats.GetCurrentHealth();
    }

    private void OnTakeDamage() {
        _slider.value = _characterStats.GetCurrentHealth();
    }

}
