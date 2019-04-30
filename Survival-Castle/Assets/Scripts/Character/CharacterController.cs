using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterController : MonoBehaviour {

    private CharacterMotor _characterMotor;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();

        _characterMotor.onStartMove += OnStartMove;
        _characterMotor.onStop += OnStop;
        _characterMotor.onDead += OnDead;
    }

    private void OnDestroy() {
        _characterMotor.onStartMove -= OnStartMove;
        _characterMotor.onStop -= OnStop;
        _characterMotor.onDead -= OnDead;
    }

    private void Start() {
        _characterMotor.Move();
    }

    private void OnStartMove() {
    }

    private void OnStop() {
        _characterMotor.Die();
    }

    private void OnDead() {
        Destroy(this.gameObject, 8f);
    }

}
