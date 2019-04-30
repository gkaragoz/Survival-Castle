using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterAnimation : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    private CharacterMotor _characterMotor;

    private const string RUN = "IsRunning";
    private const string DIE = "Die";
    private const string DEAD_SELECTOR = "INT_DeadSelector";

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

    public void OnStartMove() {
        _animator.SetBool(RUN, true);
    }

    public void OnStop() {
        _animator.SetBool(RUN, false);
    }

    public void OnDead() {
        int random = Random.Range(0, 1);
        _animator.SetInteger(DEAD_SELECTOR, random);
        _animator.SetTrigger(DIE);
    }

}
