using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private LayerMask _clickableLayerMask = 0;
    [SerializeField]
    private BaseController _baseController = null;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _clickableLayerMask)) {
                if (hit.collider.tag == "Ground") {
                    Debug.Log(hit.point);
                    _baseController.Attack(hit.point);
                }
            }
        }
    }

    public void AddCrew(bool isInitialization = false) {
        _baseController.AddCrew(isInitialization);
    }

}
