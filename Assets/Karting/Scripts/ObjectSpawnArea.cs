using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ObjectSpawnArea : MonoBehaviour {
    public float spawnInterval = 1f; // Time between spawns
    public float flingForce = 1000000f; // Force to fling objects towards player

    private List<GameObject> objectsToSpawn; // List of objects to spawn
    private GameObject player; // Reference to player object
    private int rate = 2;

    private KillPlaneTrigger killPlane;

    private void Start() {
        // Get reference to player object
        player = GameObject.FindGameObjectWithTag("Player");
        killPlane = FindObjectOfType<KillPlaneTrigger>();
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
        yield return new WaitForSeconds(3.5f);
        int c = 0;
        while (true) {
            if (killPlane.playerHit) break;
            if (c >= rate && spawnInterval > 0.4f) {
                spawnInterval = spawnInterval - 0.05f;
                rate = rate + 1;
            }
            GameObject spawnedObject = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], transform.position + new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), Random.Range(-halfDepth, halfDepth)), new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
            spawnedObject.GetComponent<Rigidbody>().AddForce(((player.transform.position + new Vector3(0,0,Random.Range(10,25)))  - spawnedObject.transform.position).normalized * flingForce);
            c++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
