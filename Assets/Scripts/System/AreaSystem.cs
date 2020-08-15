
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaSystem : MonoSingleton<AreaSystem>
{
    public Tilemap tilemap;
    Vector3Int index;
    Dictionary<Vector3Int, Area> areas = new Dictionary<Vector3Int, Area>();

    // Start is called before the first frame update
    void Awake()
    {
        //Init
        for (int i = Setting.MIN_AREA; i <= Setting.MAX_AREA; i++)
        {
            for (int j = Setting.MIN_AREA; j < Setting.MAX_AREA; j++)
            {
                Vector3Int key = new Vector3Int(i ,j ,0);
                areas[key] = new EmptyArea();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        index = tilemap.WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));
        if (tilemap.HasTile(index))
        {
            //Log("Highlight");
            //Log(index.ToString());
        }
    }

    public void ClickTile()
    {
        if (tilemap.HasTile(index))
        {
            Log("ClickTile");
        }
    }

    public bool GetTile(out Vector3Int outIndex)
    {
        if (tilemap.HasTile(index))
        {
            outIndex = index;
            return true;
        }
        outIndex = Vector3Int.zero;
        return false;
    }

    void ShowArea(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            Log("ShowArea");
            areas[index].Show();
        }
    }

    public void SetArea(Vector3Int index, Area area)
    {
        if (tilemap.HasTile(index))
        {
            Log("SetArea");
            var old = areas[index];
            old.Trigger();
            old.Clear();
            areas[index] = area;
            area.Init();
        }
    }

    public bool CanMove(Vector3Int index1, Vector3Int index2)
    {
        return true;
    }
}
