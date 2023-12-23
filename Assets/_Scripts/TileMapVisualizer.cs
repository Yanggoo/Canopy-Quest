using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floortileMap, walltileMap, ladderMap,platformMap,spikeMap;
    [SerializeField]
    private TileBase floorTile,wallTop,wallSideRight,wallSideLeft,wallBottom,wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpLeft, wallDiagonalCornerUpRight, ladder,platform,spike;

    public void PaintFloorTile(IEnumerable<Vector2Int> Positions)
    {
        PaintTiles(Positions, floortileMap, floorTile);
    }

    public void PaintLadders(IEnumerable<Vector2Int> Positions)
    {
        PaintTiles(Positions, ladderMap, ladder);
    }

    public void PaintPlatforms(IEnumerable<Vector2Int> Positions)
    {
        PaintTiles(Positions, platformMap, platform);
    }

    public void PaintSpikes(IEnumerable<Vector2Int> Positions)
    {
        PaintTiles(Positions, spikeMap, spike);
    }



    internal void PaintSingleBasicWall(Vector2Int posiitons, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if(tile!=null)
        PaintSingleTile(walltileMap, tile, posiitons);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floortileMap.ClearAllTiles();
        walltileMap.ClearAllTiles();
        ladderMap.ClearAllTiles();
        platformMap.ClearAllTiles();
        spikeMap.ClearAllTiles();


    }

    internal void PaintSingleDiagnalWall(Vector2Int posiiton, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile != null)
            PaintSingleTile(walltileMap, tile, posiiton);

    }
}
