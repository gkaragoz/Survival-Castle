﻿using UnityEngine;

public class Projectile : MonoBehaviour {

    public enum OwnerEnum {
        Base,
        Enemy
    }

    [Header("Initializations")]
    [SerializeField]
    private TrailRenderer _tr;

    [Header("Settings")]
    [SerializeField]
    private Material _projectileLineRed;
    [SerializeField]
    private Material _projectileLineGreen;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _damage = 0f;
    [Utils.ReadOnly]
    [SerializeField]
    private OwnerEnum _owner;

    private Rigidbody _rb;

    public OwnerEnum Owner {
        get { return _owner; }
    }

    public float Damage {
        get { return _damage; }
        set { _damage = value; }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (_rb.velocity.magnitude > 0) {
            _rb.rotation = Quaternion.LookRotation(_rb.velocity);
        }

        if (transform.position.y <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void SetOwner(OwnerEnum owner) {
        this._owner = owner;

        switch (Owner) {
            case OwnerEnum.Base:
                _tr.material = _projectileLineGreen;
                break;
            case OwnerEnum.Enemy:
                _tr.material = _projectileLineRed;
                break;
            default:
                _tr.material = _projectileLineRed;
                break;
        }
    }

    public void AddForce(Vector3 force) {
        _rb.AddForce(force, ForceMode.VelocityChange);
    }

}
