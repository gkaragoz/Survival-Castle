using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    private BaseTargetSelector _baseTargetSelector;
    private BaseAttacker _baseAttacker;

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
        _baseAttacker = GetComponent<BaseAttacker>();
    }

    private void Start() {
        _baseTargetSelector.StartSearchTarget();
        _baseAttacker.StartAttack();
    }

}
