using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    Image img;
    UIDragHandler dh;
    private void Awake()
    {
        img = GetComponent<Image>();
        dh = gameObject.AddComponent<UIDragHandler>();
        dh.OnDragStart += OnDragStart;
        dh.OnDragEnd += OnDragEnd;
        CardSystem.Instance.OnShowCardEvent += ShowCard;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateCard()
    {
        var c = CardSystem.Instance.currentCard;
        c.Rotate();
        ShowCard(c);
    }

    public void ShowCard(Card card)
    {
        img.sprite = card.GetTile().sprite;
    }

    public void OnDragStart()
    {
        Debug.Log("OnDragStart");
        GetComponent<Button>().enabled = false;
    }
    public void OnDragEnd()
    {
        Debug.Log("OnDragEnd");
        GameManager.Instance.CurrentPlayer.UseCard();
        GetComponent<Button>().enabled = true;
    }
}
