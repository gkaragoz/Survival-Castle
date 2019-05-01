using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Character_SO _characterDefinition_Template;

    [Header("Debug")]
    [SerializeField]
    private Character_SO _character;

    #region Initializations

    private void Awake() {
        if (_characterDefinition_Template != null) {
            _character = Instantiate(_characterDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void IncreaseHealth(float value) {
        if (GetCurrentHealth() + value >= GetMaxHealth()) {
            return;
        }

        _character.CurrentHealth += value;
    }

    #endregion

    #region Decreasers

    public void DecreaseHealth(float value) {
        _character.CurrentHealth -= value;

        if (GetCurrentHealth() <= 0) {
            _character.CurrentHealth = 0;
        }
    }

    #endregion

    #region Setters

    public void SetCurrentHealth(float amount) {
        if (amount <= 0) {
            _character.CurrentHealth = 0;
            return;
        }
        if (amount > GetMaxHealth()) {
            _character.MaxHealth = amount;
        }

        _character.CurrentHealth = amount;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _characterDefinition_Template.Name;
    }

    public GameObject GetPrefab() {
        return _characterDefinition_Template.Prefab;
    }

    public float GetCurrentHealth() {
        return _character.CurrentHealth;
    }

    public float GetMaxHealth() {
        return _character.MaxHealth;
    }

    #endregion

    #region Custom Methods

    #endregion

}
