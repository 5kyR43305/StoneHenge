using UnityEngine;

public class StoneDestroy : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float velocityThreshold = 0.05f;
    [SerializeField] private float checkDelay = 2f;
    [SerializeField] private float destroyDelay = 1f;

    private bool checking = false;
    private bool isDestroying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(StartChecking), checkDelay);
    }

    void StartChecking()
    {
        checking = true;
    }

    void Update()
    {
        if (!checking || isDestroying || rb == null) return;

        if (rb.linearVelocity.magnitude < velocityThreshold)
        {
            isDestroying = true;
            Destroy(gameObject, destroyDelay);
        }
    }
}
