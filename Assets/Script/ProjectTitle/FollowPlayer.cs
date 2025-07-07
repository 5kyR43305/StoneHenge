using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // public Transform player=null;      // Assign the Player's transform in the Inspector
    public float speed = 5f;      // Movement speed


    GameObject player;

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
