using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    Image img;
    Button btn;
    UIDragHandler dh;
    private void Awake()
    {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
        dh = gameObject.AddComponent<UIDragHandler>();
        dh.OnDragStart += OnDragStart;
        dh.OnDragEnd += OnDragEnd;
        GameManager.Instance.OnCardTurnStartEvent += () => { btn.interactable = true; dh.enabled = true; };
        GameManager.Instance.OnCardWillbeUsedEvent += () => { btn.interactable = false; dh.enabled = false; };
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
        btn.enabled = false;
    }

    public void OnDragEnd()
    {
        GameManager.Instance.WantToUseCard();
        btn.enabled = true;
    }
}
