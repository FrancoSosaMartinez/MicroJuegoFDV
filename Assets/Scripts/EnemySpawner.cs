using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidePrefab;
    public GameObject miniAsteroidePrefab;
    public float spawnRatePerMinute = 20f;
    public float spawnRateIncrement = 1f;
    public float xLimit, yLimit;
    public float maxLife = 10f;
    private float spawnNext = 0;

    void Start()
    {
        yLimit = Camera.main.orthographicSize;
        xLimit = Camera.main.aspect * yLimit;
    }

    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            int lado = Random.Range(0, 4); 
            Vector2 spawnPosition = Vector2.zero;

            switch (lado)
            {
                case 0: // arriba
                    spawnPosition = new Vector2(Random.Range(-xLimit, xLimit), yLimit);
                    break;
                case 1: // abajo
                    spawnPosition = new Vector2(Random.Range(-xLimit, xLimit), -yLimit);
                    break;
                case 2: // izquierda
                    spawnPosition = new Vector2(-xLimit, Random.Range(-yLimit, yLimit));
                    break;
                case 3: // derecha
                    spawnPosition = new Vector2(xLimit, Random.Range(-yLimit, yLimit));
                    break;
            }

            int deNTama = Random.Range(0, 2);
            GameObject prefabAInstanciar = null;
            switch (deNTama)
            {
                case 0:
                    prefabAInstanciar = asteroidePrefab;
                    break;
                case 1:
                    prefabAInstanciar = miniAsteroidePrefab;
                    break;
            }

            GameObject meteorIma = Instantiate(prefabAInstanciar, spawnPosition, Quaternion.identity);
            meteorIma.transform.position = new Vector3(meteorIma.transform.position.x,
                                            meteorIma.transform.position.y, -0.1f );



            // Código para lanzar los meteoritos al centro
            Rigidbody rb = meteorIma.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direccionCentro = (Vector3.zero - meteorIma.transform.position).normalized;
                rb.AddForce(direccionCentro * 2f, ForceMode.Impulse); 
            }

            Destroy(meteorIma, maxLife);
        }
    }
}