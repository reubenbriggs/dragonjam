using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    [System.Serializable]
    private class MapChunk
    {
        public GameObject chunkObject;
        public float chunkLength;
        public int minHeight, maxHeight;
    }

    private class SpawnedChunk
    {
        public GameObject spawnedObject;
        public float playerHeightToDestroy;

        public SpawnedChunk(GameObject obj, float height) {
            spawnedObject = obj;
            playerHeightToDestroy = height;
        }
    }

    [SerializeField]
    private List<MapChunk> chunks;
    private List<MapChunk> currentChunks;
    private List<SpawnedChunk> spawnedChunks;
    [SerializeField]
    private float minSpacing, maxSpacing;
    [SerializeField]
    private Transform mapParent;
    [SerializeField]
    private bool useMaxHeight;
    private float startPosition = -4;
    private int chunksToPrewarm = 5;
    private float lastSpawnedHeight;
    private int? lastChunkSpawnedIndex;

    // Use this for initialization
    void Start() {
        spawnedChunks = new List<SpawnedChunk>();
        lastSpawnedHeight = startPosition;

        SpawnChunks(chunksToPrewarm);
    }

    private void Update() {
        int numberToSpawn = RemoveDeadChunks();
        SpawnChunks(numberToSpawn);
    }

    private void SpawnChunks(int numberToSpawn) {
        UpdateCurrentChunks();
        int? chunkIndexToSpawn = null;
        for (int i = 0; i < numberToSpawn; i++) {
            while(chunkIndexToSpawn == null || chunkIndexToSpawn == lastChunkSpawnedIndex) {
                chunkIndexToSpawn = Random.Range(0, currentChunks.Count);
            }
            MapChunk toSpawn = currentChunks[chunkIndexToSpawn.Value];
            lastChunkSpawnedIndex = chunkIndexToSpawn;
            GameObject spawned = Instantiate(toSpawn.chunkObject, mapParent, true);
            Vector3 pos = spawned.transform.position;
            pos.y = lastSpawnedHeight + Random.Range(minSpacing, maxSpacing) + toSpawn.chunkLength / 2;
            spawned.transform.position = pos;
            spawnedChunks.Add(new SpawnedChunk(spawned, pos.y + Camera.main.orthographicSize * 2 + (toSpawn.chunkLength)));
            lastSpawnedHeight = pos.y + toSpawn.chunkLength / 2;
        }
    }

    private void UpdateCurrentChunks() {
        currentChunks = new List<MapChunk>();
        foreach (MapChunk chunk in chunks) {
            if (chunk.minHeight < lastSpawnedHeight && (!useMaxHeight || chunk.maxHeight > lastSpawnedHeight)) {
                currentChunks.Add(chunk);
            }
        }
    }

    private int RemoveDeadChunks() {
        List<SpawnedChunk> toRemove = new List<SpawnedChunk>();
        foreach(SpawnedChunk spawned in spawnedChunks) {
            if(spawned.playerHeightToDestroy < Camera.main.transform.position.y) {
                toRemove.Add(spawned);
            }
        }
        int numberRemoved = 0;
        foreach(SpawnedChunk remove in toRemove) {
            spawnedChunks.Remove(remove);
            Destroy(remove.spawnedObject);
            numberRemoved++;
        }
        return numberRemoved;
    }
}
