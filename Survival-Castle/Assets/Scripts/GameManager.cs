﻿using System;
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
    private List<CharacterController> _enemies = new List<CharacterController>();

    public Transform Target { get { return _target; } }
    public List<CharacterController> Enemies { get { return _enemies; } }

    public void AddCharacter(CharacterController enemy) {
        _enemies.Add(enemy);
        enemy.onDead += RemoveCharacter;
    }

    public void RemoveCharacter(CharacterController enemy) {
        _enemies.Remove(enemy);
        enemy.onDead -= RemoveCharacter;

        Destroy(enemy.gameObject, 2f);
    }

}
