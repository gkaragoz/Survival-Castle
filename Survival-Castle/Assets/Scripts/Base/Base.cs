using UnityEngine;

public class Base : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Base_SO _baseDefinition_Template;

    [Header("Debug")]
    [SerializeField]
    private Base_SO _base;

    #region Initializations

    private void Awake() {
        if (_baseDefinition_Template != null) {
            _base = Instantiate(_baseDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void IncreaseHealth(float value) {
        if (GetCurrentHealth() + value >= GetMaxHealth()) {
            return;
        }

        _base.CurrentHealth += value;
    }

    #endregion

    #region Decreasers

    public void DecreaseHealth(float value) {
        _base.CurrentHealth -= value;

        if (GetCurrentHealth() <= 0) {
            _base.CurrentHealth = 0;
        }
    }

    #endregion

    #region Setters

    public void SetCurrentHealth(float amount) {
        if (amount <= 0) {
            _base.CurrentHealth = 0;
            return;
        }
        if (amount > GetMaxHealth()) {
            _base.MaxHealth = amount;
        }

        _base.CurrentHealth = amount;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _baseDefinition_Template.Name;
    }

    public float GetCurrentHealth() {
        return _base.CurrentHealth;
    }

    public float GetMaxHealth() {
        return _base.MaxHealth;
    }

    #endregion

    #region Custom Methods

    #endregion

}
