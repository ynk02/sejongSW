using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Inst { get; private set; }
    void Awake()
    {
        Inst = this;    
    }
    
    [SerializeField] int startCardCount;
    public static Action<bool> onAddCard;
    
    

    public IEnumerator StartDraw()
    {
        for (int i = 0; i < startCardCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            onAddCard?.Invoke(true);
        }
    }
    
}
