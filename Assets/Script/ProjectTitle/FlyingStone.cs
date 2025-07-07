using System;
using UnityEngine;

public class FlyingStone : MonoBehaviour
{
    public static event Action OnMissionComplete;    // 발행인이 돌
    // 리스너: MainUI.  Why   button 과 Slider를 Unlock. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }
    }


}