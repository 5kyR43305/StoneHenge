using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileBinder : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private ProjectileSO projectileData;
    [SerializeField] private Transform launcherPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float launchSpeed = 10f;

    private GameObject currentProjectileInstance;

    public System.Action OnLaunchRequested;

    void Start()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument is not assigned!");
            return;
        }

        var root = uiDocument.rootVisualElement;

        var speedSlider = root.Q<SliderInt>("speed");
        var angleSlider = root.Q<SliderInt>("angle");
        var massSlider = root.Q<SliderInt>("mass");
        var launchButton = root.Q<Button>("Launch");
                
        speedSlider.value = projectileData.Speed;
        angleSlider.value = projectileData.Angle;
        massSlider.value = projectileData.Mass;


        speedSlider.RegisterValueChangedCallback(evt =>
        {
            projectileData.Speed = evt.newValue;
            UpdateCurrentProjectileSpeed();
        });

        angleSlider.RegisterValueChangedCallback(evt =>
        {
            projectileData.Angle = evt.newValue;
        });

        massSlider.RegisterValueChangedCallback(evt =>
        {
            projectileData.Mass = evt.newValue;
            UpdateCurrentProjectileMass();
        });

        launchButton.clicked += () =>
        {
            Debug.Log("Launcher Start!");
            OnLaunchRequested?.Invoke();
            SpawnProjectile();
        };
    }

    void SpawnProjectile()
    {
        currentProjectileInstance = Instantiate(projectilePrefab, launcherPoint.position, launcherPoint.rotation);
        ApplyValuesToProjectile(currentProjectileInstance);
    }

    void ApplyValuesToProjectile(GameObject proj)
    {
        var rb = proj.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.mass = projectileData.Mass;
        Vector3 launchDir = Quaternion.Euler(-projectileData.Angle, 0, 0) * launcherPoint.up;
        rb.linearVelocity = launchDir * projectileData.Speed;
    }

    void UpdateCurrentProjectileSpeed()
    {
        if (currentProjectileInstance == null) return;

        var rb = currentProjectileInstance.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 launchDir = Quaternion.Euler(-projectileData.Angle, 0, 0) * launcherPoint.up;
        rb.linearVelocity = launchDir * projectileData.Speed;
    }

    void UpdateCurrentProjectileMass()
    {
        if (currentProjectileInstance == null) return;

        var rb = currentProjectileInstance.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.mass = projectileData.Mass;
    }
}
