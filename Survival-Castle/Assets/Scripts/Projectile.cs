using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _damage = 0f;

    private Rigidbody _rb;

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
