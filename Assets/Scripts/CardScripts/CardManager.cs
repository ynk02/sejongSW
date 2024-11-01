
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;
    
    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> myCards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform LeftCard;
    [SerializeField] Transform RightCard;
    
    
    List<Item> itemBuffer;
    Card selectedCard;
    public bool isCardDrawing;
    public bool onMyCardArea;
    
    void Start()
    {
        SetupItemBuffer();
        DrawManager.onAddCard += AddCard;
    }

    void Update()
    {
        if (isCardDrawing)
        {
            CardDrag();
        }    
    }

    //카드뽑기
    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
        {
            SetupItemBuffer();
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }
    
    
    //덱 생성 및 초기화
    void SetupItemBuffer()
    {
        //아이템들 일괄 추가
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            itemBuffer.Add(item);
        }
        
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void OnDestroy()
    {
        DrawManager.onAddCard -= AddCard;
    }
    
    
    //카드 추가 함수
    void AddCard(bool check)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.SetUp(PopItem());
        myCards.Add(card);
        SetOriginOrder();
        CardAlignment();
    }

    void CardAlignment()
    {
        
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(LeftCard, RightCard,myCards.Count,2.0f,Vector3.one*1.9f);
        
        
        var targetCards = myCards;
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            
            targetCard.orginPRS=originCardPRSs[i];
            targetCard.MoveTransform(targetCard.orginPRS,true,0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>();

        switch (objCount)
        {
            case 1:
                objLerps = new float[] { 0.5f };
                break;
            case 2:
                objLerps = new float[] { 0.27f, 0.73f };
                break;
            case 3:
                objLerps = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            default:
                float interval = 1.0f / (objCount - 1);
                for (int i = 0; i < objCount; i++) objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Quaternion.identity;
            if (objCount>=4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i]*4 - 2.0f, 2));
                if (height < 0) curve *= -1;
                targetPos.y+=curve;
                targetRot=Quaternion.Slerp(rightTr.rotation, leftTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));

        }
        return results;
    }

    public bool TryUnitSpawn()
    {
        var card =selectedCard;
        var spawnPos = Utils.MousePos;
        var targetCards= myCards;
        if (UnitSpawnManager.Inst.SpawnUnit(spawnPos, card))
        {
            targetCards.Remove(card);
            card.transform.DOKill();
            DestroyImmediate(card.gameObject);
            selectedCard = null;
            CardAlignment();
            return true;
        }
        else
        {

            foreach (var nowCard in targetCards)
            {
                nowCard.GetComponent<Order>().SetMostFrontOrder(false);
            }
            CardAlignment();
            return false;
        }
    }
    
    public void SetOriginOrder()
    {
        int count=myCards.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        selectedCard = card;
        EnLargeCard(true,card);
    }
    public void CardMouseOut(Card card)
    {
        EnLargeCard(false,card);
    }
    
    public void CardMouseDown()
    {
        isCardDrawing = true;
    }

    public void CardMouseUp()
    {
        isCardDrawing = false;
        TryUnitSpawn();
    }

    public void CardDrag()
    {
        if (!onMyCardArea)
        {
            selectedCard.MoveTransform(new PRS(Utils.MousePos,Utils.QI,selectedCard.orginPRS.scale),false);
        }
    }

    public void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos,Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits,x=>x.collider.gameObject.layer==layer);
        
    }
    public void EnLargeCard(bool isLarge, Card card)
    {
        if (isLarge)
        {
            Vector3 enLargePos = new Vector3(card.orginPRS.pos.x, -3.0f, -10f);
            card.MoveTransform(new PRS(enLargePos, Utils.QI, Vector3.one*2.5f),false);
        }
        else
        {
            card.MoveTransform(card.orginPRS,false);
        }
        card.GetComponent<Order>().SetMostFrontOrder(isLarge);
        
    }
    
    #endregion
}
