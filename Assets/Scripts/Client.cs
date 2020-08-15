using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public TurnType activeType;
    List<Entity> entityList = new List<Entity>();
    Card currentCard;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.OnTurnStart += OnTurnStart;
        GameManager.Instance.OnTurnEnd += OnTurnEnd;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTurnStart()
    {
        enabled = GameManager.IsPlayer(activeType);
        if (enabled)
        {
            currentCard = CardSystem.Instance.ShowCard();
            entityList.ForEach((a) => a.IsFinishMove = false);
        }
    }

    void OnTurnEnd()
    {
        enabled = false;
    }

    public void RotateCard()
    {
        currentCard.Rotate();
    }

    public void UseCard()
    {
        Vector3Int index;
        if (!AreaSystem.Instance.GetTile(out index))
            return;
        CardSystem.Instance.UseCard(currentCard, index);
    }

    public void MoveEntity(int id, Vector3Int index)
    {
        if (AreaSystem.Instance.CanMove(entityList[id].CurrentAreaIndex, index))
        {
            EntitySystem.Instance.Move(entityList[id], index);
        }
        OnMoveEndCheck();
    }

    public void OnMoveEndCheck()
    {
        if (entityList.TrueForAll((a)=>a.IsFinishMove))
        {
            GameManager.Instance.MoveEnd();
        }
    }
}
