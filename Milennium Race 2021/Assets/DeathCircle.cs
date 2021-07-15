using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathCircle : MonoBehaviour
{
    Vector3 startScale;
    float radius;
    bool growing;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        radius = Random.Range(1, 2);
        speed = Random.Range(0.1f, 1f);
        growing = true;
        startScale = transform.localScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (growing)
        {
            transform.localScale += new Vector3(speed, speed, 0) * Time.deltaTime;
            if (transform.localScale.x > startScale.x + radius)
            {
                growing = false;
            }
        }
        else 
        {
            transform.localScale -= new Vector3(speed, speed, 0) * Time.deltaTime;
            if (transform.localScale.x < startScale.x)
            {
                growing = true;
            }
        }
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
