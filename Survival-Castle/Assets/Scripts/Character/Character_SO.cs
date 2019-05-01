using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Character";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maxHealth;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
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