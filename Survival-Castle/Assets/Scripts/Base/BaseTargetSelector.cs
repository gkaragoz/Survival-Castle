using UnityEngine;

[RequireComponent(typeof(BaseAttacker))]
public class BaseTargetSelector : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _selectedTarget;

    private BaseAttacker _baseAttacker;
    private Base _baseStats;

    public bool HasTarget { get { return _selectedTarget == null ? false : true; } }
    public CharacterController SelectedTarget { get { return _selectedTarget; } }

    private void Awake() {
        _baseAttacker = GetComponent<BaseAttacker>();
        _baseStats = GetComponent<Base>();
    }

    public void SearchTarget() {
        int enemyCount = GameManager.instance.Enemies.Length;
        if (enemyCount <= 0) {
            return;
        }

        CharacterController closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int ii = 0; ii < enemyCount; ii++) {
            CharacterController potantialTarget = GameManager.instance.Enemies[ii];
            if (potantialTarget.IsDead) {
                if (potantialTarget == _selectedTarget) {
                    _selectedTarget = null;
                }
                continue;
            }

            if (!potantialTarget.gameObject.activeInHierarchy) {
                continue;
            }

            float potantialDistance = Vector3.Distance(transform.position, potantialTarget.transform.position);
            if (potantialDistance <= closestDistance) {
                closestTarget = potantialTarget;
                closestDistance = potantialDistance;

                _selectedTarget = closestTarget;
            }
        }
    }

    public void DeselectTarget() {
        _selectedTarget = null;
    }

}
