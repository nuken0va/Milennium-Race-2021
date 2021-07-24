using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public bool IsDangerous;
    public Transform[] Ps;
    private void Start()
    {
        foreach (var ps in Ps)
        { 
            var shape = ps.GetComponent<ParticleSystem>().shape;
            print(shape.scale);
            print(transform.localScale.x);
            shape.scale = new Vector3(transform.localScale.x, 1, 1);
            ps.gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && IsDangerous)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
        }
    }
}
