using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyableLayer : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    private Tilemap destroyableTileMap;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;
    private Vector3 pos6;
    private Vector3 pos7;
    private Vector3 pos8;
    // Start is called before the first frame update
    void Start()
    {
        destroyableTileMap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitPos = collision.bounds.ClosestPoint(collision.transform.position);
            pos1 = new Vector3(hitPos.x+offsetX, hitPos.y, 0.0f);
            pos2 = new Vector3(hitPos.x- offsetX, hitPos.y, 0.0f);
            pos3 = new Vector3(hitPos.x, hitPos.y+offsetY, 0.0f);
            pos4 = new Vector3(hitPos.x, hitPos.y- offsetY, 0.0f);
            pos5 = new Vector3(hitPos.x+ offsetX, hitPos.y+ offsetY, 0.0f);
            pos6 = new Vector3(hitPos.x+ offsetX, hitPos.y- offsetY, 0.0f);
            pos7 = new Vector3(hitPos.x- offsetX, hitPos.y+ offsetY, 0.0f);
            pos8 = new Vector3(hitPos.x- offsetX, hitPos.y- offsetY, 0.0f);

            Vector3Int position = destroyableTileMap.WorldToCell(pos1);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos2);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos3);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos4);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos5);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos6);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos7);
            destroyableTileMap.SetTile(position, null);
            position = destroyableTileMap.WorldToCell(pos8);
            destroyableTileMap.SetTile(position, null);

            Destroy(collision.gameObject);

        }
    }
}
