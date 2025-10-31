using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO", order = int.MaxValue)]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private Sprite itemImg;

    public string ItemName
    {
        get
        {
            return itemName;
        }
        set
        {
            itemName = value;
        }
    }

    public Sprite ItemImg
    {
        get
        {
            return itemImg;
        }
        set
        {
            itemImg = value;
        }
    }


}