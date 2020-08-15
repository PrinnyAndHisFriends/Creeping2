using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool isActive = true;
    private bool isFinishMove = false;

    public bool IsFinishMove { get => isActive && isFinishMove; set => isFinishMove = value; }
    public Vector3Int CurrentAreaIndex { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        //Init currentAreaIndex
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {

    }
}
