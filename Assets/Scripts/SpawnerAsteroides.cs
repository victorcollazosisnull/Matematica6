using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroides : MonoBehaviour
{
    [Header("Configuracion de Spawner de Asteroides")]
    [SerializeField] private GameObject prefabAsteroide;
    [SerializeField] private float _time;
    private float _timeSpawnear;

    void Update()
    {
        if (Time.time >= _timeSpawnear)
        {
            SpawnAsteroide();
            _timeSpawnear = Time.time + _time; 
        }
    }
    public void SpawnAsteroide()
    {
        float randomX = Random.Range(-53.7f, 53.7f); 
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 58.5f); 
        Instantiate(prefabAsteroide, spawnPosition, Quaternion.identity);
    }
}
