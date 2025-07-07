using System;
using System.Collections;
using UnityEngine;
public enum StoneType
{
    Low,
    Middle,
    High
}
public class TargetStone : MonoBehaviour
{
    public static event Action<StoneType> OnHitByProjectile;
    public static event Action<StoneType> OnKnockDownEvent;
    public StoneType stoneType;
    public Renderer objRenderer;

    float fadeDuration = 2f;
    Color originalColor;
    bool isHit = false;
      
    private void Start()
    {

        originalColor = objRenderer.material.color;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            OnHitByProjectile?.Invoke(stoneType);
            isHit = true;
        }
    }

    void Update()
    {
        if (isHit)
        {          

            if (Mathf.Abs(transform.eulerAngles.z) == 270 || Mathf.Abs(transform.eulerAngles.z) == 90)
            {
                OnKnockDownEvent?.Invoke(stoneType);
                StartCoroutine(FadeOutObject());
                isHit = false;
            }
        }
    }

    IEnumerator FadeOutObject()
    {
        float elapsed = 0f;
        while (elapsed < 2)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            objRenderer.material.color = newColor;
            elapsed += Time.deltaTime;
            yield return null;
        }       
        Color finalColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        objRenderer.material.color = finalColor;
        yield return new WaitForSeconds(1);
       
        Destroy(gameObject);
    }


}
