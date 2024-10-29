using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    public Item item;
    public PRS orginPRS;

    public void SetUp(Item item)
    {
        this.item = item;

        //character.sprite = this.item.sprite;
        //nameTMP.text = this.item.name;
    }

    public void MoveTransform(PRS prs, bool useDoTween, float dotweenTime = 0)
    {
        if (useDoTween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
