using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldGenerator : MonoBehaviour
{
    public GameObject EnemyShield;
    public Transform SpawnPoint;
    public float time = 1.5f;
    void Start()
    {
        StartCoroutine(MakeEnemy());
    }
    
    public IEnumerator MakeEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            Instantiate(EnemyShield,SpawnPoint);
        }
    }
}