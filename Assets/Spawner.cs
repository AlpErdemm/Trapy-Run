using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyObject;
    public bool isSpawning = false;
    float lastSpawned = 0f;
    float spawnPeriod = 0.5f;

    void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
        RoundManager.PlayerDied += OnPlayerDie;
        RoundManager.FinishCrossed += OnFinishCross;
    }

    void OnRoundStart()
    {
        isSpawning = true;
    }

    private void Update()
    {
        if (isSpawning && Time.time > lastSpawned)
        {
            float positionX = Random.Range(-5f, 5f);
            float positionZ = FindObjectOfType<PlayerController>().transform.parent.position.z - 20f;

            GameObject go = Instantiate(EnemyObject, new Vector3(positionX, 1f, positionZ), Quaternion.identity, null);
            go.GetComponentInChildren<EnemyController>().OnRoundStart();
            lastSpawned = Time.time + spawnPeriod;
        }
    }

    private void OnPlayerDie()
    {
        isSpawning = false;
    }

    private void OnFinishCross()
    {
        isSpawning = false;
        EnemyController [] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
            enemy.Stop();
    }

}
