using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallSpawnerScript : MonoBehaviour
{
    public Transform prefab;
    public int offset = 30;

    public float recycleOffset = 250;
    public float startPositionX = 50;

    private float nextPositionX;
    private Queue<Transform> objectQueue;

    public GameObject player;

    void spawnWall()
    {
        var gap = Random.Range(10f, 20f);
        var av = 19.0f/*- offset*/ - gap / 2;
        Transform o = (Transform)Instantiate(prefab,
            new Vector3(nextPositionX, Random.Range(-av, av), 0),
            Quaternion.identity);
        var obst = o.GetComponent<WallObstacleScript>();
        obst.isDangerous = (Random.value >= 0.5f);
        obst.isMoving = (Random.value >= 0.5f);
        obst.moveSpeed = Random.value >= 0.5f ? -1 : 1 * Random.Range(3f, 5f);
        obst.gap = gap;
        objectQueue.Enqueue(o);
    }

    void Start()
    {
        nextPositionX = startPositionX;

        objectQueue = new Queue<Transform>();

        for (; nextPositionX < player.transform.position.x + recycleOffset; nextPositionX += offset)
        {
            spawnWall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (objectQueue.Peek().position.x + recycleOffset < player.transform.position.x)
        {
            Transform o = objectQueue.Dequeue();
            Destroy(o.gameObject);
        }


        for (; nextPositionX < player.transform.position.x + recycleOffset; nextPositionX += offset)
        {
            spawnWall();
        }
    }
}
