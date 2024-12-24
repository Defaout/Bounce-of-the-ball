using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson : MonoBehaviour
{
    [SerializeField] private Block[] _blocks; // Двумерный массив блоков
    [SerializeField] private SpawnBlockOnMap _spawnBlockOnMap;
    public void StartLoad()
    {
      StartCoroutine(LoadMap());
    }
    IEnumerator LoadMap()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Map1.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataObject blockData = JsonConvert.DeserializeObject<DataObject>(json);
            ParseBlocks(blockData);
        }
        else
        {
            Debug.LogError("JSON file not found: " + path);
        }
        yield return null;
    }
    void ParseBlocks(DataObject blockData)
    {
        int width = blockData.d[0].Length;
        _blocks = new Block[width];

        for (int y = 0; y < width; y++)
        {
            int blockId = blockData.d[0][y];
            _blocks[y] = CreateBlock(blockData, y,blockId, blockData.e);
        }
        _spawnBlockOnMap.SpawnMapFromJsonArray(_blocks);
    }

    private Block CreateBlock(DataObject blockData,int _blockNum,int id, Dictionary<string, BlockStats> stats)
    {
        Block block =  new Enemy();
        switch (id)
        {
            case 6: // Турель
                block = new Turret
                {
                    _id = id,
                    _hp = blockData.d[1][_blockNum],
                    _armor = blockData.d[2][_blockNum],
                    _damage = stats[id.ToString()].dmg
                };
                break;
            case 7: // Враг
                block = new Enemy
                {
                    _id = id,
                    _hp = blockData.d[1][_blockNum],
                    _armor = blockData.d[2][_blockNum],
                    _damage = stats[id.ToString()].dmg,
                    _fallDamage = stats[id.ToString()].fall_dmg
                };
                break;
            default: // Обычный блок
                block = new Block
                {
                    _id = id,
                    _hp = blockData.d[1][_blockNum],
                    _armor = blockData.d[2][_blockNum]
                };
                break;
        }
        return block;
    }
}

[Serializable]
public class DataObject
{
    public int[][] d; // Массив массивов
    public Dictionary<string, BlockStats> e; // Словарь вложенных объектов
}
[Serializable]
public class Block
{
    public int _id; // ID блока
    public int _armor; // Броня блока 
    public int _hp; // Здоровье блока 
}

[Serializable]
public class Turret : Block
{
    public int _damage; // Урон турели
}

[Serializable]
public class Enemy : Block
{
    public int _damage; // Урон врага
    public int _fallDamage; // Урон при падении 
}

[Serializable]
public class BlockStats
{
    public int dmg; // Урон
    public int fall_dmg; // Урон при падении 
}