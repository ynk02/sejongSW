using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public int speed = 5;
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        
        position.x -= speed * Time.deltaTime;
        
        transform.position = position;
    }
}
