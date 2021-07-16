using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public bool IsDangerous;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("dead!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
