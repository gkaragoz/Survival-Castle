using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor), typeof(CharacterAttacker))]
public class CharacterAnimation : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    private CharacterController _characterController;
    private CharacterMotor _characterMotor;
    private CharacterAttacker _characterAttacker;

    private const string RUN = "IsRunning";
    private const string DIE = "Die";
    private const string DEAD_SELECTOR = "INT_DeadSelector";
    private const string ATTACK = "Attack";

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttacker = GetComponent<CharacterAttacker>();

        _characterMotor.onStartMove += OnStartMove;
        _characterMotor.onStop += OnStop;
        _characterController.onDead += OnDead;
        _characterAttacker.onAttack += OnAttack;
    }

    private void OnDestroy() {
        _characterMotor.onStartMove -= OnStartMove;
        _characterMotor.onStop -= OnStop;
        _characterController.onDead -= OnDead;
        _characterAttacker.onAttack -= OnAttack;
    }

    public void OnStartMove() {
        _animator.SetBool(RUN, true);
    }

    public void OnStop() {
        _animator.SetBool(RUN, false);
    }

    public void OnAttack() {
        _animator.SetTrigger(ATTACK);
    }

    public void OnDead(CharacterController characterController) {
        int random = Random.Range(0, 2);
        _animator.SetInteger(DEAD_SELECTOR, random);
        _animator.SetTrigger(DIE);
    }

}
