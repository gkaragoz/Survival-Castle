using UnityEngine;
using UnityEngine.UI;

public class OverlayHealthBar : Menu {

    [SerializeField]
    private float _positionOffset = 0f;

    private Slider _slider;

    private Camera _camera;

    private CharacterController _characterController;

    private void Awake() {
        _camera = Camera.main;
        _slider = GetComponent<Slider>();
    }

    private void LateUpdate() {
        if (_characterController == null) {
            return;
        }

        transform.position = _camera.WorldToScreenPoint(_characterController.transform.position + Vector3.up * _positionOffset);
    }

    public void Initialize(CharacterController characterController) {
        this._characterController = characterController;

        _characterController.onDead += OnDead;
        _characterController.onTakeDamage += OnTakeDamage;
        _characterController.onReused += OnReused;

        _slider.maxValue = _characterController.MaxHealth;
        _slider.value = _characterController.CurrentHealth;
    }

    private void OnReused() {
        _slider.maxValue = _characterController.MaxHealth;
        _slider.value = _characterController.CurrentHealth;

        Show();
    }

    private void OnTakeDamage() {
        _slider.value = _characterController.CurrentHealth;
    }

    private void OnDead(CharacterController character) {
        Hide();
    }

}
