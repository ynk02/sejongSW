using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 50;

    void TakeDamage(int value)
    {
        health -= value;
    }

    public float GetHealth()
    {
        return health;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
