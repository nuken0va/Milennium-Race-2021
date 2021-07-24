using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public CarSelector carSelector;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
        carSelector.Subscribe(this);
    }

}
