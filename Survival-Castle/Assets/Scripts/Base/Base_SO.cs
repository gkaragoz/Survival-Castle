using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "Scriptable Objects/Base")]
public class Base_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Base";

    // Health
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maxHealth;

    // Attack
    [SerializeField]
    [Range(1, 89)]
    private float _shootAngle = 45f;
    [SerializeField]
    private float _attackRange = 20f;
    [SerializeField]
    private float _minAttackRange = 15f;
    [SerializeField]
    private float _maxAttackRange = 40f;
    [SerializeField]
    private float _attackRate = 0.5f;
    [SerializeField]
    private float _maxAttackRate = 20f;
    [SerializeField]
    private float _attackDamage = 50f;
    [SerializeField]
    private float _minAttackDamage = 50f;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public float CurrentHealth {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float MaxHealth {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float ShootAngle {
        get { return _shootAngle; }
        set { _shootAngle = value; }
    }

    public float AttackRange {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public float MinAttackRange {
        get { return _minAttackRange; }
        set { _minAttackRange = value; }
    }

    public float MaxAttackRange {
        get { return _maxAttackRange; }
        set { _maxAttackRange = value; }
    }

    public float AttackRate {
        get { return _attackRate; }
        set { _attackRate = value; }
    }

    public float MaxAttackRate {
        get { return _maxAttackRate; }
        set { _maxAttackRate = value; }
    }

    public float AttackDamage {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    public float MinAttackDamage {
        get { return _minAttackDamage; }
        set { _minAttackDamage = value; }
    }

    #endregion

}
