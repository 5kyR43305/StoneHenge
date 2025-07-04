using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isFalling = false;

    [SerializeField] private float fallThreshold = 0.5f;
    [SerializeField] private float resetDelay = 3f;
    [SerializeField] private GameObject wallPrefab;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float dot = Vector3.Dot(transform.up, Vector3.up);

        if (dot < fallThreshold && !isFalling)
        {
            isFalling = true;
            StartCoroutine(ResetAfterDelay());
        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        Instantiate(wallPrefab, originalPosition, originalRotation);

        Destroy(gameObject);
    }
}
