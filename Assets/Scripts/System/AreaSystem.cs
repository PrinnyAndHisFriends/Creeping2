
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaSystem : MonoSingleton<AreaSystem>
{
    public Tilemap tilemap;
    public GameObject lightPrefab;
    public GameObject highlightPrefab;
    Vector3Int pointedIndex;
    public Vector3 mousePosInWorld;

    List<Area> areaList = new List<Area>();
    Dictionary<Vector3Int, Area> areas = new Dictionary<Vector3Int, Area>();
    Dictionary<Vector3Int, GameObject> lights = new Dictionary<Vector3Int, GameObject>();
    Dictionary<Vector3Int, GameObject> highlights = new Dictionary<Vector3Int, GameObject>();
    IAreaModeStrategy originalMode = new NormalMode();
    IAreaModeStrategy areaMode = new NormalMode();


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Ant]; i++)
            areaList.Add(new AntArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Rotate]; i++)
            areaList.Add(new RotateArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Exchange]; i++)
            areaList.Add(new ExchangeArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.EPresent]; i++)
            areaList.Add(new EpresentArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Gap]; i++)
            areaList.Add(new GapArea());
        for (int i = 0; i < Setting.areaDeckCount[Setting.AreaType.Grass]; i++)
            areaList.Add(new GrassArea());

        for (int i = Setting.MIN_AREA; i <= Setting.MAX_AREA; i++)
        {
            for (int j = Setting.MIN_AREA; j <= Setting.MAX_AREA; j++)
            {
                Vector3Int key = new Vector3Int(i, j, 0);
                if (tilemap.HasTile(key))
                {
                    lights[key] = Instantiate(lightPrefab, GetWorldPosition(key), Quaternion.identity, transform);
                    highlights[key] = Instantiate(highlightPrefab, GetWorldPosition(key), Quaternion.identity, transform);
                    lights[key].SetActive(false);
                    highlights[key].SetActive(false);

                    if (key == Setting.houseStartIndex)
                    {
                        areas[key] = new HouseArea();
                        areas[key].Trigger(key);
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

    Vector3Int lastHighlightIndex;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePosInWorld = ray.GetPoint(-ray.origin.z / ray.direction.z);
        pointedIndex = tilemap.WorldToCell(mousePosInWorld);

        UnHighlight(lastHighlightIndex);
        areaMode.Highlight(pointedIndex);

        if (Input.GetMouseButtonDown(0))
        {
            ClickArea();
        }
    }

    public void SetMode(IAreaModeStrategy mode)
    {
        areaMode = mode;
    }

    public void Highlight(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            highlights[index].SetActive(true);
            lastHighlightIndex = index;
        }
    }

    public void UnHighlight(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            highlights[index].SetActive(false);
        }
    }
    public void Light(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            lights[index].SetActive(true);
        }
    }
    public void Unlight(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            lights[index].SetActive(false);
        }
    }


    #region Area
    void ShowArea(Vector3Int index)
    {
        if (tilemap.HasTile(index))
        {
            Log("ShowArea");
            areas[index].ShowForward(tilemap, index);
        }
    }

    public event Action OnFinishEvent;
    public void TriggerArea(Vector3Int index, Action OnFinish)
    {
        if (tilemap.HasTile(index))
        {
            OnFinishEvent = OnFinish;
            var old = areas[index];
            Log("TriggerArea: " + old.GetType().ToString());
            old.ShowForward(tilemap, index);
            old.Trigger(index);
            //BUG: normal mode 下 end 比 start 快
            Log(areaMode.GetType().ToString() + " Start");
            areaMode.Start();
        }
    }
    public void OnTriggerAreaFinish()
    {
        Log(areaMode.GetType().ToString() + " End");
        areaMode.End();
        areaMode = originalMode;
        OnFinishEvent?.Invoke();
    }

    public bool HasArea(Vector3Int index)
    {
        return tilemap.HasTile(index) ;
    }

    public Area GetArea(Vector3Int index)
    {
        return areas.ContainsKey(index) ? areas[index] : null;
    }

    public void SetArea(Vector3Int index, Area area)
    {
        if (tilemap.HasTile(index))
        {
            Log("SetArea");
            areas[index] = area;
            area.ShowForward(tilemap, index);
        }
    }
    public void ClickArea()
    {
        if (tilemap.HasTile(pointedIndex))
        {
            areaMode.OnAreaClick(pointedIndex);
            Log("ClickTile");
        }
    }

    #endregion

    public void ForeachArea(Func<Vector3Int, bool> func, Action<Vector3Int> action)
    {
        foreach (var data in areas)
        {
            if (func(data.Key))
            {
                action(data.Key);
            }
        }
    }

    public bool CanMove(Vector3Int index1, Vector3Int index2)
    {
        return index1 != index2;
    }

    public bool GetMouseIndex(out Vector3Int outIndex)
    {
        if (tilemap.HasTile(pointedIndex))
        {
            outIndex = pointedIndex;
            return true;
        }
        outIndex = Vector3Int.zero;
        return false;
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

public interface IAreaModeStrategy
{
    void Highlight(Vector3Int index);
    void OnAreaClick(Vector3Int index);
    void Start();
    void End();
}

public class NormalMode : IAreaModeStrategy
{
    public void End()
    {
    }

    public void Highlight(Vector3Int index)
    {
        AreaSystem.Instance.Highlight(index);
    }

    public void OnAreaClick(Vector3Int index)
    {
        //throw new NotImplementedException();
    }

    public void Start()
    {
    }
}
public class RotateMode : IAreaModeStrategy
{
    public void End()
    {
        AreaSystem.Instance.ForeachArea( a=> false, AreaSystem.Instance.Unlight);
    }

    public void Highlight(Vector3Int index)
    {
        AreaSystem.Instance.Highlight(index);
    }

    public void OnAreaClick(Vector3Int index)
    {
        if (CanSelect(index))
        {
            var way = AreaSystem.Instance.GetArea(index) as IWayArea;
            way.Rotate();
            AreaSystem.Instance.SetArea(index, way as Area);
            AreaSystem.Instance.OnTriggerAreaFinish();
        }
    }

    public void Start()
    {
        AreaSystem.Instance.ForeachArea(CanSelect, AreaSystem.Instance.Light);
    }
    public bool CanSelect(Vector3Int index)
    {
        var current = AreaSystem.Instance.GetArea(index);
        return current.IsShowed/* && current is IWayArea && !EntitySystem.Instance.HasEntity(index)*/;
    }
}
public class ExchangeMode : IAreaModeStrategy
{
    Vector3Int first;
    bool isFirst = true;
    public void End()
    {
        AreaSystem.Instance.ForeachArea(a => false, AreaSystem.Instance.Unlight);
    }

    public void Highlight(Vector3Int index)
    {
        AreaSystem.Instance.Highlight(index);
    }

    public void OnAreaClick(Vector3Int index)
    {
        if (!CanSelect(index))
            return;
        if (isFirst)
        {
            first = index;
            isFirst = false;
        }
        else
        {
            var secondArea = AreaSystem.Instance.GetArea(index);
            var firstArea = AreaSystem.Instance.GetArea(first);
            AreaSystem.Instance.SetArea(first, secondArea);
            AreaSystem.Instance.SetArea(index, firstArea);
            AreaSystem.Instance.OnTriggerAreaFinish();
        }
    }

    public void Start()
    {
        AreaSystem.Instance.ForeachArea(CanSelect, AreaSystem.Instance.Light);
    }

    public bool CanSelect(Vector3Int index)
    {
        var current = AreaSystem.Instance.GetArea(index);
        return current.IsShowed /*&& !(current is HouseArea)*//* && !EntitySystem.Instance.HasEntity(index)*/;
    }
}