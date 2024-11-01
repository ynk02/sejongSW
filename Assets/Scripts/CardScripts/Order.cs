using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] midRenderers;
    [SerializeField] string sortingLayerName;
    public int originOrder;

    public void SetOriginOrder(int order)
    {
        this.originOrder = order;
        SetOrder(order);
    }
    
    public void SetMostFrontOrder(bool isFront)
    {
        SetOrder(isFront ? 100 : originOrder);
    }

    public void SetOrder(int order)
    {
        int mulOrder = order*10;
        
        foreach(var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }

        foreach (var renderer in midRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder+1;
        }
    }
}
