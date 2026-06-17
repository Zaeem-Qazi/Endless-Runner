using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] ObstaclePrefab;
    private float TimeUntilObstacleSpawn;
    public float SpawnTimer = 2.5f;
    public float ObstacleSpeed = 4f;
    private float _SpawnTimer;
    private float _ObstacleSpeed;
    [Range(0,1)] public float _SpawnTimerFactor = 0.1f;
    [Range(0, 1)] public float _ObstacleSpeedFactor = 0.2f;

    [SerializeField] private Transform ObstacleParent;

    private float TimeAlive;
    // Update is called once per frame
    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
    
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }
    void Update()
    {
        if (GameManager.Instance.isplaying)
        {
            TimeAlive += Time.deltaTime;
            Calculate();
            spawnloop();
        }
    }

    private void spawnloop()
    {
        TimeUntilObstacleSpawn += Time.deltaTime;
        if (TimeUntilObstacleSpawn >= _SpawnTimer)
        {
            spawn();
            TimeUntilObstacleSpawn = 0;

        }
    }

    private void Calculate()
    {
        _SpawnTimer = SpawnTimer / Mathf.Pow(TimeAlive, _SpawnTimerFactor);
        _ObstacleSpeed = ObstacleSpeed * Mathf.Pow(TimeAlive, _ObstacleSpeedFactor);
    }

    private void ResetFactors()
    {
        TimeAlive = 1f;
       
        _ObstacleSpeed = ObstacleSpeed;
        _SpawnTimer = SpawnTimer; 
    }
    private void ClearObstacles()
    {
         foreach ( Transform child in ObstacleParent)
        {
            Destroy(child.gameObject);
        }
    }
        private void spawn()
    {
        GameObject ObstacleToSpawn = ObstaclePrefab[Random.Range(0, ObstaclePrefab.Length)];
        GameObject SpawnObstacle = Instantiate(ObstacleToSpawn, transform.position, Quaternion.identity);
        SpawnObstacle.transform.parent = ObstacleParent;

        Rigidbody2D ObstacleRb =  SpawnObstacle.GetComponent<Rigidbody2D>();
        ObstacleRb.velocity = Vector2.left * _ObstacleSpeed;
    }
    
}
