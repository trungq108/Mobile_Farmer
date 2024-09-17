using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Transform world;
    [SerializeField] private Chunk chunkPrefab;

    private int chunkIndex;
    private const string CHUNKINDEX = "chunkIndex";
    private const string CHUNK = "Chunk_";

    private void OnEnable()
    {
        EventManager.AddListener<CreatNewChunk>(CreatNewChunkCallBack);
              
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CreatNewChunk>(CreatNewChunkCallBack);

    }

    private void Start()
    {
        LoadWorld();
    }

    public void LoadWorld()
    {
        if (!ES3.KeyExists(CHUNKINDEX))
        {
            CreatNewChunk e = new CreatNewChunk();
            e.newChunkPosition = Vector3.zero;
            EventManager.TriggerEvent(e);
        }
        else
        {
            chunkIndex = ES3.Load<int>(CHUNKINDEX);
            Debug.Log(chunkIndex);
            for (int i = 0; i < chunkIndex; i++)
            {
                Chunk chunk = Instantiate(chunkPrefab, world);
                chunk.LoadChunk(CHUNK + i);
            }
        }
    }

    public void CreatNewChunkCallBack(CreatNewChunk e)
    {
        Chunk chunk = Instantiate(chunkPrefab, world);
        chunk.transform.position = e.newChunkPosition;
        chunk.transform.rotation = e.newChunkRotation;
        chunk.OnInit();
    }

    [Button]
    public void SaveWorld()
    {
        chunkIndex = 0;
        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();
            chunk.SaveChunk(CHUNK + i);
            chunkIndex++;
        }
        Debug.Log(chunkIndex);
        ES3.Save<int>(CHUNKINDEX, chunkIndex);
    }

    private void OnApplicationQuit()
    {
        SaveWorld();
    }
}
