using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 scaleAddition;
    private float step;
    public Vector3 scaleFactor = new Vector3(1,1,1);
    public float speed;
    void Start()
    {
        originalScale = transform.localScale;
        scaleAddition = new Vector3(originalScale.x * scaleFactor.x, 
                                  originalScale.y * scaleFactor.y, 
                                  originalScale.z * scaleFactor.z) - originalScale;
    }

    Vector3 NexStep(Vector3 original, Vector3 target, float step)
    {
        var factor = (Mathf.Cos(step) + 1) / 2f;
        return new Vector3(
            original.x + factor * target.x,
            original.y + factor * target.y,
            original.z + factor * target.z
            );
    }

    void Update()
    {
        step += Time.deltaTime * speed;
        transform.localScale = NexStep(originalScale, scaleAddition, step);
    }
}
