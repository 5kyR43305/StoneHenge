using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument myUI;
    [SerializeField] ProjectitleLauncher Launcher;
    [SerializeField] Transform launchingPad;
    VisualElement root;

    Button launchButton;
    Slider angleSlider;
    Slider speedSlider;
    Slider massSlider;

    private void Awake()
    {
        root = myUI.rootVisualElement;

        speedSlider = root.Q<Slider>("speed");
        angleSlider = root.Q<Slider>("angle");
        massSlider = root.Q<Slider>("mass");
        launchButton = root.Q<Button>("Launch");

        launchButton.clicked += OnThrowButtonClick;
        angleSlider.value = launchingPad.transform.eulerAngles.z;

        angleSlider.RegisterValueChangedCallback(evt =>
        {
            float angle = evt.newValue;
            launchingPad.transform.rotation = Quaternion.Euler(0, 0, angle);
        });

        speedSlider.RegisterValueChangedCallback(evt =>
        {
            Launcher.launchSpeed = evt.newValue;
        });

        massSlider.RegisterValueChangedCallback(evt =>
        {
            if (Launcher.projectile != null)
            {
                Rigidbody rb = Launcher.projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.mass = evt.newValue;
                }
            }
        });
    }

    private void OnThrowButtonClick()
    {
        Launcher.ThrowStone();
        LockButtonSAndSlider();
        StartCoroutine(UnlockAfterDelay(2f)); // Start coroutine to unlock after 2 seconds
    }

    void LockButtonSAndSlider()
    {
        launchButton.SetEnabled(false);
        speedSlider.SetEnabled(false);
        angleSlider.SetEnabled(false);
        massSlider.SetEnabled(false);
    }

    void UnLockButtonSAndSlider()
    {
        launchButton.SetEnabled(true);
        speedSlider.SetEnabled(true);
        angleSlider.SetEnabled(true);
        massSlider.SetEnabled(true);
    }

    IEnumerator UnlockAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnLockButtonSAndSlider();
    }
}
