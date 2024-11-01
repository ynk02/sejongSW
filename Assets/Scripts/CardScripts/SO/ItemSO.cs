using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;
    public int attack;
    public int health;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "ItemSO",menuName = "Scriptable Objects/ItemSO")]

public class ItemSO : ScriptableObject
{
    public Item[] items;
}