using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    [SerializeField] ProjectileSO projectileData;
    [SerializeField] Transform launcherPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform LaunchingPad;

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
        var speedSlider = root.Q<Slider>("speed");
        var angleSlider = root.Q<Slider>("angle");
        var massSlider = root.Q<Slider>("mass");
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
            float angle = evt.newValue;
            LaunchingPad.transform.rotation = Quaternion.Euler(0, 0, angle);
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
