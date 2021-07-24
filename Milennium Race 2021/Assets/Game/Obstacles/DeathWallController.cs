using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathWallController : MonoBehaviour
{
    public float startSpeed = 1f;
    private float currentSpeed;
    public float acceleration = 0.001f;
    public float maxSpeed = 30f;

    private GameObject player;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startSpeed,0);
        currentSpeed = startSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(currentSpeed, 0);
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
        }
        if (player.transform.position.x - 200f > transform.position.x)
        {
            rb.MovePosition(new Vector2(player.transform.position.x - 200f, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
        }
    }
}
