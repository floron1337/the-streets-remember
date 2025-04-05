using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewRadius = 10f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Debug")]
    public bool drawGizmos = true;

    public bool CanSeePlayer()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider target in targetsInView)
        {
            if (target.CompareTag("Player"))
            {
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

                // --- ADD THE DEBUG/VISION CODE HERE ---
                if (angleToTarget < viewAngle / 2f)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
                    Vector3 rayOrigin = transform.position + Vector3.up * 1f; // Eye level

                    // Longer-lasting, thicker debug rays
                    Debug.DrawRay(rayOrigin, dirToTarget * dstToTarget, Color.cyan, 2f, true); // 2 sec duration
                    if (!Physics.Raycast(rayOrigin, dirToTarget, dstToTarget, obstacleMask))
                    {
                        Debug.DrawRay(rayOrigin, dirToTarget * dstToTarget, Color.green, 2f, true);
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(rayOrigin, dirToTarget * dstToTarget, Color.red, 2f, true);
                    }
                }
                // --- END OF ADDED CODE ---
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}