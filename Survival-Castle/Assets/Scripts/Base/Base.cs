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

    public void IncreaseAttackRate(float value) {
        _base.AttackRate -= value;

        if (GetAttackRate() <= GetMaxAttackRate()) {
            _base.AttackRate = GetMaxAttackRate();
        }
    }

    public void IncreaseAttackDamage(float value) {
        _base.AttackDamage += value;
    }

    public void IncreaseAttackRange(float value) {
        _base.AttackRange += value;

        if (GetAttackRange() >= GetMaxAttackRange()) {
            _base.AttackRange = GetMaxAttackRange();
        }
    }

    #endregion

    #region Decreasers

    public void DecreaseHealth(float value) {
        _base.CurrentHealth -= value;

        if (GetCurrentHealth() <= 0) {
            _base.CurrentHealth = 0;
        }
    }

    public void DecreaseAttackRate(float value) {
        _base.AttackRate += value;
    }

    public void DecreaseAttackDamage(float value) {
        _base.AttackDamage -= value;

        if (GetAttackDamage() <= GetMinAttackDamage()) {
            _base.AttackDamage = GetMinAttackDamage();
        }
    }

    public void DecreaseAttackRange(float value) {
        _base.AttackRange -= value;

        if (GetAttackRange() <= GetMinAttackRange()) {
            _base.AttackRange = GetMinAttackRange();
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

    public float GetShootAngle() {
        return _base.ShootAngle;
    }

    public float GetAttackRange() {
        return _base.AttackRange;
    }

    public float GetMaxAttackRange() {
        return _base.MaxAttackRange;
    }

    public float GetMinAttackRange() {
        return _base.MinAttackRange;
    }

    public float GetAttackRate() {
        return _base.AttackRate;
    }

    public float GetMaxAttackRate() {
        return _base.MaxAttackRate;
    }

    public float GetAttackDamage() {
        return _base.AttackDamage;
    }

    public float GetMinAttackDamage() {
        return _base.MinAttackDamage;
    }

    #endregion

    #region Custom Methods

    #endregion

}
