using UnityEngine;

public class Projectile : MonoBehaviour {

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        _rb.rotation = Quaternion.LookRotation(_rb.velocity);
    }

}
