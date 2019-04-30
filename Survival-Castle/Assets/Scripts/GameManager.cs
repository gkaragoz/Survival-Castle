using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private Transform _target;
    private List<GameObject> _enemies = new List<GameObject>();

    public Transform Target { get { return _target; } }

    public void AddCharacter(GameObject enemy) {
        _enemies.Add(enemy);
    }

    public void RemoveCharacter(GameObject enemy) {
        _enemies.Remove(enemy);
        Destroy(enemy, 2f);
    }

}
