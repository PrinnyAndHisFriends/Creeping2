using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3Int index = tilemap.WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));
        if (tilemap.HasTile(index))
        {
            Debug.LogError(111);
            tilemap.SetTile(index, tile);
        }
    }
}
