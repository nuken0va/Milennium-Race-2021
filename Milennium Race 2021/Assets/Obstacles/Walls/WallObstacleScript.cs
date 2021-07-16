using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacleScript : MonoBehaviour
{
    public Transform dangerousWallPrefab;
    public Transform wallPrefab;
    //public float width = 1;
    //public float gap = 10;
    //public bool isDangerous = false;
    
    public bool isMoving = false;
    public float moveSpeed = 0.5f;
    public float offset = 0f;
    [SerializeField] float border = 18f;

    public Transform topWall, bottomWall;
    private void RemoveAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tr = transform.GetChild(i);
            GameObject.Destroy(tr.gameObject);
        }
    }
    [SerializeField] private bool _isDangerous;
    public bool isDangerous
    {
        get { return _isDangerous; }
        set
        {
            _isDangerous = value;
            if (value)
            {
                RemoveAllChildren();
                topWall = Instantiate(dangerousWallPrefab,
                    new Vector3(0, dangerousWallPrefab.transform.localScale.y / 2 + gap / 2, 0),
                    Quaternion.identity,
                    this.transform
                    );
                bottomWall = Instantiate(dangerousWallPrefab,
                    new Vector3(0, - (dangerousWallPrefab.transform.localScale.y / 2 + gap / 2), 0),
                    Quaternion.identity,
                    this.transform
                    );
            }
            else
            {
                RemoveAllChildren();
                topWall = Instantiate(wallPrefab,
                    new Vector3(0, wallPrefab.transform.localScale.y / 2 + gap / 2, 0),
                    Quaternion.identity,
                    this.transform
                    );
                bottomWall = Instantiate(wallPrefab,
                    new Vector3(0, -(dangerousWallPrefab.transform.localScale.y / 2 + gap / 2), 0),
                    Quaternion.identity,
                    this.transform
                    );
            }

        }
    }
    [SerializeField] private float _gap;
    public float gap
    {
        get => _gap;
        set
        {
            _gap = value;
            topWall.localPosition = new Vector3(0, wallPrefab.transform.localScale.y / 2 + _gap / 2, 0);
            bottomWall.localPosition = new Vector3(0, -(dangerousWallPrefab.transform.localScale.y / 2 + _gap / 2), 0);
        }
    }
    private float _width;
    public float width
    {
        get { return _width; }
        set 
        {
            _width = value;
            topWall.localScale = new Vector3(_width, topWall.localScale.y, topWall.localScale.z);
            bottomWall.localScale = new Vector3(_width, bottomWall.localScale.y, bottomWall.localScale.z);
        }
    }

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //isDangerous = false;
        width = 5;
        //gap = 10;
        //var av = border - offset - gap / 2;
        //transform.position = new Vector3(transform.position.x, Random.Range(-av, av), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isMoving) 
        {
            if (Mathf.Abs(transform.position.y) > border - offset - gap / 2)
            {
                moveSpeed *= -1;
            }
            rb.velocity = new Vector2(0, moveSpeed);
        }

    }
}
