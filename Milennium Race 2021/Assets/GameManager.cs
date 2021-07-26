using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    public GameObject wall;
    public GameObject AliveHUD;
    public GameObject DeathHUD;
    public List<GameObject> PlayerPrefabs;

    private DateTime startTime;

    public void Awake()
    {
        GameObject selectedCar;
        if(PlayerPrefabs.Count > GameInfo.selectedCar)
        {
            selectedCar = PlayerPrefabs[GameInfo.selectedCar];
        }
        else
        {
            selectedCar = PlayerPrefabs[0];
        }
        player = Instantiate(selectedCar, new Vector3(0, 0, 0), Quaternion.Euler(0,0,-90));
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        startTime = DateTime.Now;
    }

    public void EndGame(int deathType = -1)
    {
        player.GetComponent<CarController>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        wall.GetComponent<DeathWallController>().enabled = false;
        wall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Particle"))
        {
            obj.GetComponent<ParticleSystem>().Pause();
        }
        foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        AliveHUD.SetActive(false);
        DeathHUD.SetActive(true);
       
        var score = (int)((player.transform.position.x + 30) / 60);
        var carId = GameInfo.selectedCar;
        var time  = (float)(DateTime.Now - startTime).TotalSeconds;
        GameInfo.AddScore(score, carId, time, deathType);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
