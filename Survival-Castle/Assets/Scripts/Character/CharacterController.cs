using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterController : MonoBehaviour {

    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();

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
        _characterAttacker.Attack();
        _characterMotor.Die();
    }

    private void OnDead() {
        GameManager.instance.RemoveCharacter(this.gameObject);
    }

}
