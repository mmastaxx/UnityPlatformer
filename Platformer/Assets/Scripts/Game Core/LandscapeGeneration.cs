using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandscapeGeneration : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] TileBase[] topLevelTiles;
    [SerializeField] List<TileBase> middleLevelTiles;
    [SerializeField] List<TileBase> lowLevelTiles;
    [SerializeField] Tilemap groundTileMap;
    int[,] map;
    void Start()
    {
        TerrainGeneration();
    }

    void TerrainGeneration()
    {
        map = GenerateArray(width, height,false);
        RenderMap(map, groundTileMap, topLevelTiles);
    }
    int[,] GenerateArray(int width, int height, bool isEmpty)
    {
        int[,] map = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = (isEmpty) ? 0 : 1;
            }
        }
        return map;
    }
    void RenderMap(int[,] map, Tilemap groundMap, TileBase[] tileBase)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(map[i, j] == 1) 
                    groundMap.SetTile(new Vector3Int(i, j, 0), tileBase[UnityEngine.Random.Range(0, tileBase.Length)])  ;
            }
        }
    }
}
