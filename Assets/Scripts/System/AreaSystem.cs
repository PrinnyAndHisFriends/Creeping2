
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaSystem : MonoSingleton<AreaSystem>
{
    public Tilemap tilemap;
    Vector3Int index;
    public Vector3 mousePosInWorld;

    List<Area> areaList = new List<Area>();
    Dictionary<Vector3Int, Area> areas = new Dictionary<Vector3Int, Area>();

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Ant]; i++)
            areaList.Add(new AntArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Rotate]; i++)
            areaList.Add(new RotateArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Exchange]; i++)
            areaList.Add(new ExchangeArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Present]; i++)
            areaList.Add(new PresentArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Gap]; i++)
            areaList.Add(new GapArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Grass]; i++)
            areaList.Add(new GrassArea());

        for (int i = Setting.MIN_AREA; i <= Setting.MAX_AREA; i++)
        {
            for (int j = Setting.MIN_AREA; j <= Setting.MAX_AREA; j++)
            {
                Vector3Int key = new Vector3Int(i ,j ,0);
                if (key == Setting.houseStartIndex)
                {
                    areas[key] = new HouseArea();
                    areas[key].ShowForward(tilemap, key);
                    continue;
                }
                else if (key == Setting.girlStartIndex)
                {
                    areas[key] = new Way1_2_3_4_5_6Area(TileManager.Instance.way1_2_3_4_5_6);
                    areas[key].ShowForward(tilemap, key);
                    continue;
                }
                else if (key == Setting.antStartIndex)
                {
                    areas[key] = new GrassArea();
                    areas[key].ShowForward(tilemap, key);
                    continue;
                }


                if (tilemap.HasTile(key))
                {
                    int id = UnityEngine.Random.Range(0, areaList.Count);
                    var data = areaList[id];
                    areaList.RemoveAt(id);
                    areas[key] = data;
                }
            }
        }

        if (areaList.Count != 0)
            Debug.LogError(ToString() + "Area count error" + areaList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePosInWorld = ray.GetPoint(-ray.origin.z / ray.direction.z);
        index = tilemap.WorldToCell(mousePosInWorld);
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
            areas[index].ShowForward(tilemap, index);
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
            area.ShowForward(tilemap, index);
        }
    }

    public bool CanMove(Vector3Int index1, Vector3Int index2)
    {
        return true;
    }

    public Vector3 GetWorldPosition(Vector3Int index)
    {
        return tilemap.GetCellCenterWorld(index);
    }

    public Vector3Int GetIndex(Vector3 worldPos)
    {
        return tilemap.WorldToCell(worldPos);
    }
}
