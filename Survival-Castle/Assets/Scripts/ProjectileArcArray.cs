using UnityEngine;

/// <summary>
/// Referenced from https://en.wikipedia.org/wiki/Trajectory_of_a_projectile
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class ProjectileArcArray : MonoBehaviour {

    [Header("Settings")]
    [SerializeField]
    private float _testVelocity;
    [SerializeField]
    private float _degreeAngle = 45f;
    [SerializeField]
    private int _resolution = 10;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _gravity; // Force of gravity on Y axis.
    [SerializeField]
    [Utils.ReadOnly]
    private float _radianAngle;

    private LineRenderer _lr;

    private void Awake() {
        _lr = GetComponent<LineRenderer>();

        _gravity = Mathf.Abs(Physics.gravity.y);

        _radianAngle = Mathf.Deg2Rad * _degreeAngle;
    }

    // Create an array of Vector3 positions for arc.
    private Vector3[] CalculateArcArray(Vector3 targetPosition) {
        Vector3[] arcArray = new Vector3[_resolution + 1];

        Quaternion angleBetweenTarget = GetAngleBetweenTarget(targetPosition);
        float velocity = GetForceToHitTarget(targetPosition);
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * _radianAngle)) / _gravity;

        for (int ii = 0; ii <= _resolution; ii++) {
            float t = (float)ii / (float)_resolution;
            arcArray[ii] = CalculateArcPoint(t, maxDistance, velocity, angleBetweenTarget);
        }

        return arcArray;
    }

    // Calculate height and distance of each vertex.
    private Vector3 CalculateArcPoint(float t, float maxDistance, float velocity, Quaternion angleBetweenTarget) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(_radianAngle) - ((_gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(_radianAngle) * Mathf.Cos(_radianAngle)));

        return angleBetweenTarget * Quaternion.Euler(0, -90, 0) * new Vector3(x, y);
    }

    // Rotate itself to target.
    private Quaternion GetAngleBetweenTarget(Vector3 targetPosition) {
        Vector3 direction = targetPosition - transform.position;
        return Quaternion.LookRotation(direction, Vector3.up);
    }

    // Get required force to hit the target on X, Y.
    private float GetForceToHitTarget(Vector3 targetPosition) {
        float distance = Vector3.Distance(targetPosition, transform.position);
        return Mathf.Sqrt(distance * _gravity / Mathf.Sin(2 * _radianAngle));
    }

    // Get final force vector calculations to hit the target.
    public Vector3 MagicShoot(Vector3 targetPosition) {
        float forceAmount = GetForceToHitTarget(targetPosition);
        Quaternion angleBetweenTarget = GetAngleBetweenTarget(targetPosition);

        return angleBetweenTarget * Quaternion.Euler(-_degreeAngle, 0, 0) * Vector3.forward * forceAmount;
    }

    // Show lineRenderer.
    public void SetVisibility(bool visibility) {
        _lr.enabled = visibility;
    }

    // Populating the lineRenderer with the appropriate settings.
    public void SetArcPoints(Vector3 targetPosition) {
        _lr.positionCount = _resolution + 1;
        _lr.SetPositions(CalculateArcArray(targetPosition));
    }

}
