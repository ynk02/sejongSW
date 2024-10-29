
using System;
using System.Collections;
using System.Collections.Generic;
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
    
    
    void Start()
    {
        SetupItemBuffer();
        DrawManager.onAddCard += AddCard;
    }

    void OnDestroy()
    {
        DrawManager.onAddCard -= AddCard;
    }

    void Update()
    {
        
    }
    
    //카드 추가 함수
    void AddCard(bool check)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.SetUp(PopItem());
        myCards.Add(card);
        
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

}
