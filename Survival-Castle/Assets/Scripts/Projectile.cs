using UnityEngine;

public class Projectile : MonoBehaviour {

    public enum OwnerEnum {
        Base,
        Enemy
    }

    [Header("Settings")]

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
        set { _owner = value; }
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
    }

    public void AddForce(Vector3 force) {
        _rb.AddForce(force, ForceMode.VelocityChange);
    }

}
