using UnityEngine;

[RequireComponent(typeof(BaseTargetSelector))]
public class BaseController : MonoBehaviour {

    private BaseTargetSelector _baseTargetSelector;

    private void Awake() {
        _baseTargetSelector = GetComponent<BaseTargetSelector>();
    }

    private void Start() {
        _baseTargetSelector.StartSearchTarget();
    }

}
