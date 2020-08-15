﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    UIDragHandler dh;
    private void Awake()
    {
        dh = gameObject.AddComponent<UIDragHandler>();
        dh.OnDragEnd += OnDragEnd;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string lastTexturePath;
    public void RotateCard()
    {
        var c = CardSystem.Instance.currentCard;
        c.Rotate();
        string path = c.GetTexturePath();
        if (lastTexturePath != path)
        {
            GetComponent<Image>().sprite =
                ResourceInterface.LoadSprite(path);
        }
    }

    public void OnDragEnd()
    {
        GameManager.Instance.CurrentPlayer.UseCard();
    }
}
