using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ObjectSpawnArea : MonoBehaviour {
    public float spawnInterval = 0.5f; // Time between spawns
    public float flingForce = 1000f; // Force to fling objects towards player

    private List<GameObject> objectsToSpawn; // List of objects to spawn
    private GameObject player; // Reference to player object

    private void Start() {
        // Get reference to player object
        player = GameObject.FindGameObjectWithTag("Player");
        objectsToSpawn = new List<GameObject>();
        LoadObjects();

        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update() {}

    private void LoadObjects() {
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/BowlingBallPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/DonutPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/DumbbellPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/HammerPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/IceCreamPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/MissilePrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/ToiletPrefab"));
    }

    IEnumerator SpawnObjects() {
        while (true) {
            // Randomly select object to spawn
            int objectIndex = Random.Range(0, objectsToSpawn.Count);

            // Spawn object at random position within cube area
            float halfWidth = transform.localScale.x / 2f;
            float halfHeight = transform.localScale.y / 2f;
            float halfDepth = transform.localScale.z / 2f;
            float x = Random.Range(-halfWidth, halfWidth);
            float y = Random.Range(-halfHeight, halfHeight);
            float z = Random.Range(-halfDepth, halfDepth);
            Vector3 spawnPosition = transform.position + new Vector3(x, y, z);
            GameObject spawnedObject = Instantiate(objectsToSpawn[objectIndex], spawnPosition, Quaternion.identity);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            
            // Calculate direction to player and fling object towards player
            Vector3 directionToPlayer = player.transform.position - spawnPosition;
            rb.AddForce(directionToPlayer.normalized * flingForce);

            // Wait for spawn interval before spawning next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
