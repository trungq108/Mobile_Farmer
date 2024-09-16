using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Transform world;
    [SerializeField] private Chunk chunkPrefab;

    private List<Chunk> worldChunks = new List<Chunk>();
    private const string WORLDCHUNKS = "worldChunks";
    private const string CHUNK = "Chunk_";


    private void Awake()
    {
        LoadWorld();
    }

    public void LoadWorld()
    {
        if (!ES3.KeyExists(WORLDCHUNKS))
        {
            Chunk chunk = Instantiate(chunkPrefab, world);
            chunk.transform.position = Vector3.zero;
            chunk.transform.rotation = Quaternion.identity;
            chunk.LoadChunk(CHUNK + 0);
            worldChunks.Add(chunk);
        }
        else
        {
            worldChunks = ES3.Load<List<Chunk>>(WORLDCHUNKS);
            for (int i = 0; i < worldChunks.Count; i++)
            {
                Chunk chunk = Instantiate(chunkPrefab, world);
                chunk.LoadChunk(CHUNK + i);
            }
        }
    }

    [Button]
    public void SaveWorld()
    {
        worldChunks.Clear();
        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();
            chunk.SaveChunk(CHUNK + i);
            worldChunks.Add(chunk);
        }

        ES3.Save<List<Chunk>>(WORLDCHUNKS, worldChunks);
    }

    // DEBUG
    [Button]
    private void ResetGame()
    {
        ES3.DeleteFile();
    }
}
