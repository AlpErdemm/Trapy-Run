using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyObject;
    public bool isSpawning = false;
    float lastSpawned = 0f;
    float spawnPeriod = 0.2f;


    public int maxPoolSize = 20;
    public int stackDefaultCapacity = 20;

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

            GameObject go = TakeFromPool();

            if (go != null)
            {
                go.transform.parent.gameObject.SetActive(true);
                go.transform.parent.position = new Vector3(positionX, 1f, positionZ);
                go.GetComponent<EnemyController>().OnRoundStart();
            }
            else 
            {
                go = Instantiate(EnemyObject, new Vector3(positionX, 1f, positionZ), Quaternion.identity, null);
                go.GetComponentInChildren<EnemyController>().OnRoundStart();
            }
           
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

    public Stack<GameObject> Pool
    {
        get
        {
            if (_pool == null)
                _pool = new Stack<GameObject>();
            return _pool;
        }
    }

    private Stack<GameObject> _pool = new Stack<GameObject>();

    public void ReturnToPool(GameObject go)
    {
        go.transform.parent.gameObject.SetActive(false);
        if (_pool.Count > maxPoolSize)
            Destroy(go);
        else
            _pool.Push(go);
    }

    public GameObject TakeFromPool()
    {
        if (_pool.Count > 0)
            return _pool.Pop();
        else
            return null;
    }

}
