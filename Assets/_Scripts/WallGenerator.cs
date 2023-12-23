using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int>floorPositions,TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPosiitons = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(tileMapVisualizer, basicWallPosiitons,floorPositions);
        CreateCornerWall(tileMapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWall(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var posiiton in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionList)
            {
                var neighbourPosition = posiiton + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleDiagnalWall(posiiton, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> basicWallPosiitons, HashSet<Vector2Int> floorPositions)
    {
        foreach (var posiiton in basicWallPosiitons)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = posiiton + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleBasicWall(posiiton, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighourPosition = position + direction;
                if (floorPositions.Contains(neighourPosition) == false)
                {
                    wallPositions.Add(neighourPosition);
                }
            }
        }
        return wallPositions;
    }
}
