using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongGenerator : MonoBehaviour
{
    public GameObject EnemyLong;
    public Transform SpawnPoint;
    public float time = 2.0f;
    void Start()
    {
        StartCoroutine(MakeEnemy());
    }
    
    public IEnumerator MakeEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            Instantiate(EnemyLong,SpawnPoint);
        }
    }
}