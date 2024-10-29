using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortGenerator : MonoBehaviour
{
    public GameObject EnemyShort;
    public Transform SpawnPoint;
    public float time = 1.0f;
    void Start()
    {
        StartCoroutine(MakeEnemy());
    }
    
    public IEnumerator MakeEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            Instantiate(EnemyShort,SpawnPoint);
        }
    }
}
