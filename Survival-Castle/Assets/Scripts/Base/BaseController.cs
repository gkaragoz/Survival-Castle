using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    private BaseTargetSelector _baseTargetSelector;
    private BaseAttacker _baseAttacker;

    public bool IsSearchingTarget { get { return _baseTargetSelector.IsSearchingTarget; } }
    public bool IsAttacking { get { return _baseAttacker.IsAttacking; } }
    public float AttackRange { get { return _baseAttacker.AttackRange; } }
    public float AttackRate { get { return _baseAttacker.AttackRange; } }
    public float AttackDamage { get { return _baseAttacker.AttackDamage; } }
    public bool HasTarget { get { return _baseTargetSelector.HasTarget; } }
    public CharacterController SelectedTarget { get { return _baseTargetSelector.SelectedTarget; } }
    public float SearchRate { get { return _baseTargetSelector.SearchRate; } }

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
        _baseAttacker = GetComponent<BaseAttacker>();
    }

    private void Start() {
        _baseTargetSelector.StartSearchTarget();
    }

}
