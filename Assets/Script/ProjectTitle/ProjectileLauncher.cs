using UnityEngine;
using UnityEngine.UIElements;

public class ProjectTitleLauncher : MonoBehaviour
{
    public Transform launcherPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    [Header("****Trajectory Display****")]
    public LineRenderer lineRenderer;
    public int linePoints = 100;
    public float timeIntervallnPoints = 0.01f;

    void Update()
    {
        if (lineRenderer != null)
        {
            if (Input.GetMouseButton(1))
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
                lineRenderer.enabled = false;
        }
        
    }
    void DrawTrajectory()
    {
        Vector3 origin = launcherPoint.position;
        Vector3 startVelocity = launchSpeed * launcherPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;

        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);

            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervallnPoints;
        }
    }
}
