using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 5f;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            player.transform.position, speed * Time.deltaTime);
        }
    }
}

