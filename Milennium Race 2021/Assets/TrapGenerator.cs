using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    public Transform prefab;
    public int countPerRow = 3;
    public int offset = 5;

    public float recycleOffset = 250;
    public float startPositionX = 0;

    private float nextPositionX;
    private Queue<Transform> objectQueue;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        nextPositionX = startPositionX;

        objectQueue = new Queue<Transform>();

        for (; nextPositionX < player.transform.position.x + recycleOffset; nextPositionX += offset)
        {
            for (int i = 0; i < countPerRow; i++)
            {
                Transform o = (Transform)Instantiate(prefab,
                new Vector3(nextPositionX, Random.Range(-18, 18), 0),
                Quaternion.identity);
                objectQueue.Enqueue(o);
            }
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
            for (int i = 0; i < countPerRow; i++)
            {
                Transform o = (Transform)Instantiate(prefab,
                new Vector3(nextPositionX, Random.Range(-18, 18), 0),
                Quaternion.identity);
                objectQueue.Enqueue(o);
            }
        }
    }
}
