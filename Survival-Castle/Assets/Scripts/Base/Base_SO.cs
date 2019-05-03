using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "Scriptable Objects/Base")]
public class Base_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Base";

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maxHealth;

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

    #endregion

}
