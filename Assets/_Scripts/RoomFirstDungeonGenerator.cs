using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    protected int minRoomWidth = 4, minroomHeight = 4;
    [SerializeField]
    protected int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    protected int offset = 1;
    [SerializeField]
    protected bool randomWalkRooms = false;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    protected float lerpParameter = 0.5f;
    [SerializeField]
    protected int corridorWidthMin=3;
    [SerializeField]
    protected int corridorWidthMax=5;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    protected void CreateRooms()
    {
        var roomList = ProveduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minroomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRandomRoom(roomList);
        }
        else
        {
            floor = CreateSimpleRooms(roomList);
        }
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenters.Add(new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y)));
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualizer.PaintFloorTile(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizer);
    }


    private HashSet<Vector2Int> CreateRandomRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomList.Count; i++)
        {
            var roomBound = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBound.center.x), Mathf.RoundToInt(roomBound.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters,roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x>=(roomBound.xMin+offset)&& position.x <= (roomBound.xMax - offset)&& position.y >= (roomBound.yMin + offset) && position.y <= (roomBound.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentCenter = roomCenters[Random.Range(0, corridors.Count)];
        roomCenters.Remove(currentCenter);
        while (roomCenters.Count > 0)
        {
            Vector2Int closestCenter = FindClosestPointTo(currentCenter, roomCenters);
            roomCenters.Remove(closestCenter);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentCenter, closestCenter,corridorWidthMin,corridorWidthMax,lerpParameter);
            currentCenter = closestCenter;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentCenter, Vector2Int destination, int corridorWidthMin, int corridorWidthMax,float lerpParameter)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentCenter;
        var corridorWidth = Random.Range(corridorWidthMin, corridorWidthMax + 1);
        corridor.Add(position);
        while (position != destination)
        {
            //corridorWidth = (int)Mathf.Lerp(corridorWidth, Random.Range(corridorWidthMin, corridorWidthMax + 1), lerpParameter);
            corridorWidth = Random.Range(corridorWidthMin, corridorWidthMax + 1);

            Vector2Int directionNext= Direction2D.cardinalDirectionsList[0];
            var distance = Vector2.Distance(position, destination);
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (distance > Vector2.Distance(position + direction, destination))
                {
                    directionNext = direction;
                    distance = Vector2.Distance(directionNext+position, destination);
                }

            }
            for (int length = (int)(-corridorWidth / 2); length <= (int)(corridorWidth / 2); length++)
            {
                foreach (var dir in Direction2D.cardinalDirectionsList)
                {
                    if (dir != directionNext)
                        corridor.Add(position + dir * length);
                }
            }
            position = position + directionNext;

        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closestCorridor = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var center in roomCenters)
        {
            float currentDistance = Vector2.Distance(currentCenter, center);
            if (length > currentDistance)
            {
                length = currentDistance;
                closestCorridor = center;
            }
        }
        return closestCorridor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {

                for (int row = offset; row < room.size.y - offset; row++)
                {
                    floor.Add(new Vector2Int(room.min.x + col, room.min.y + row));
                }
            }
        }
        return floor;
    }
}
