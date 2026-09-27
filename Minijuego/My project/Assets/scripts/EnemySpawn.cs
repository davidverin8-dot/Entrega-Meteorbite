using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    private float spawnNext = 0f;
    public float xLimit = 5.5f;

    void Start()
    {
        spawnNext = Time.time + 60 / spawnRatePerMinute;
    }

    void Update()
    {
        if(!Jugador.Game_paused)
        {
            GenerarMeteorito();
        }
    }

    private void GenerarMeteorito()
    {
        if(Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);
            Vector3 spawnPosition = new Vector3(rand, 8f, 0.5f);

            GameObject meter = PoolEnemy.SharedInstance.GetPooledObject();
             
            if (meter != null) 
            {
                meter.transform.position = spawnPosition;
                meter.SetActive(true);
            }
        }
    }
}