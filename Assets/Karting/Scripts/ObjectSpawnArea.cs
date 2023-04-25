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
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/BowlingBallPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/DonutPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/DumbbellPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/HammerPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/IceCreamPrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/MissilePrefab"));
        objectsToSpawn.Add(Resources.Load<GameObject>("Obstacles/ToiletPrefab"));

        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects() {
        float halfWidth = transform.localScale.x / 2f;
        float halfHeight = transform.localScale.y / 2f;
        float halfDepth = transform.localScale.z / 2f;
        while (true) {
            GameObject spawnedObject = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], transform.position + new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), Random.Range(-halfDepth, halfDepth)), Quaternion.identity);
            spawnedObject.GetComponent<Rigidbody>().AddForce(((player.transform.position + new Vector3(0,0,Random.Range(0,10)))  - spawnedObject.transform.position).normalized * flingForce);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
