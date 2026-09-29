using UnityEngine;

public sealed class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private Transform _checkPoint;

    [SerializeField, Min(0f)]
    private float _checkRadius = 0.1f;

    [SerializeField]
    private LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        if (_checkPoint == null)
        {
            Debug.LogError(
                $"{nameof(GroundDetector)}: Check Point is not assigned.",
                this);

            enabled = false;
            return;
        }

        if (_checkRadius <= 0f)
        {
            Debug.LogError(
                $"{nameof(GroundDetector)}: Check Radius must be greater than zero.",
                this);

            enabled = false;
            return;
        }

        if (_groundLayer.value == 0)
        {
            Debug.LogError(
                $"{nameof(GroundDetector)}: Ground Layer is not assigned.",
                this);

            enabled = false;
        }
    }

    public void Refresh()
    {
        IsGrounded = Physics2D.OverlapCircle(
            _checkPoint.position,
            _checkRadius,
            _groundLayer) != null;

        Debug.Log(
        $"Grounded: {IsGrounded} | " +
        $"Player Y: {_checkPoint.root.position.y:F4} | " +
        $"Check Y: {_checkPoint.position.y:F4}");
    }

    private void OnDrawGizmosSelected()
    {
        if (_checkPoint == null)
        {
            return;
        }

        Gizmos.color = IsGrounded
            ? Color.green
            : Color.red;

        Gizmos.DrawWireSphere(
            _checkPoint.position,
            _checkRadius);
    }
}