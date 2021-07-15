using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    public Transform prefab;
    public float recycleOffset;
    public int numberOfObjects;
    public Vector3 topStartPosition;

    private Vector3 topNextPosition;
    private Queue<Transform> objectQueue;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        topNextPosition = topStartPosition;
        recycleOffset = prefab.localScale.x * 2.5f;
        objectQueue = new Queue<Transform>();

        for (int i = 0; i < numberOfObjects; i++)
        {
            {
                Transform o = (Transform)Instantiate(prefab, topNextPosition, Quaternion.identity);
                objectQueue.Enqueue(o);
            }
            var bottomPosition = topNextPosition;
            bottomPosition.y *= -1;
            {
                Transform o = (Transform)Instantiate(prefab, bottomPosition, Quaternion.identity);
                objectQueue.Enqueue(o);
            }
            topNextPosition.x += prefab.localScale.x;

        }
    }

    // Update is called once per frame
    void Update()
    {
        while(objectQueue.Peek().position.x + recycleOffset < player.transform.position.x)
        {
            {
                Transform o = objectQueue.Dequeue();
                o.position = topNextPosition;
                objectQueue.Enqueue(o);
            }
            {
                Transform o = objectQueue.Dequeue();
                o.position = new Vector3(topNextPosition.x, -topNextPosition.y, topNextPosition.z);
                objectQueue.Enqueue(o);
            }
            topNextPosition.x += prefab.localScale.x;
        }
    }
}
