using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();    // 아이템들을 저장할 리스트
    public ItemSO[] itemData;   // Scriptable Object 배열
    public GameObject outLinePrefab;

    private GameObject prevObj;
    private GameObject curObj;

    void Start()
    {
        prevObj = null;
        curObj = null;

        AddItem(itemData[0]);
        AddItem(itemData[1]);
        AddItem(itemData[2]);

        CheckItem();
    }

    public void AddItem(ItemSO itemSo)
    {
        if (itemList.Count >= 10)  // 인벤토리에 아이템 10개 초과 시 아이템 획득 불가
        {
            Debug.Log("Item FULL!!!");
            return;
        }

        GameObject newItem = new GameObject(itemSo.ItemName);
        newItem.transform.parent = this.transform;

        GameObject outLine = Instantiate(outLinePrefab);  // 아이템이 선택되었을 때 표시할 외각선 추가
        outLine.GetComponent<Transform>().SetParent(newItem.transform);
        //outLine.transform.parent = newItem.transform;
        outLine.GetComponent<Image>().enabled = false;

        Image sprite = newItem.AddComponent<Image>();   // 인벤토리에 아이템이 표시되도록 이미지 변경
        sprite.sprite = itemSo.ItemImg;

        Item item = newItem.AddComponent<Item>();  // Scriptable Object 데이터 값 전달     
        item.ItemName = itemSo.ItemName;
        item.ItemImg = itemSo.ItemImg;

        Button selectEvt = newItem.AddComponent<Button>();  // 인벤토리 아이템 상호작용 리스너
        selectEvt.onClick.AddListener(SelectItem);

        itemList.Add(item);    // Scriptable Object에 있던 정보들을 저장하여 리스트에 추가
        itemList.TrimExcess(); // 리스트 재정리
    }

    public void UseItem(string name)
    {
        if (itemList.Find(items => items.ItemName == name) == null)   // 사용할 아이템이 없는 경우 처리
        {
            Debug.Log("No Item to Use");
            return;
        }
        
        GameObject temp = GameObject.Find(name);    // 아이템 오브젝트 삭제
        Destroy(temp);

        itemList.Remove(itemList[itemList.FindIndex(items => items.ItemName.Equals(name))]); // 리스트에서 삭제
        itemList.TrimExcess();     // 리스트 재정리
        CheckItem();
    }

    public void SelectItem()
    {
        curObj = EventSystem.current.currentSelectedGameObject;
        if (prevObj == null)
        {
            prevObj = curObj;
            curObj.transform.GetChild(0).GetComponent<Image>().enabled = true;
            return;
        }
        if (prevObj == curObj)
        {
            curObj.transform.GetChild(0).GetComponent<Image>().enabled = false;
            prevObj = null;
        }
        else
        {
            SwitchItem(curObj.GetComponent<Transform>(), prevObj.GetComponent<Transform>());
            prevObj.transform.GetChild(0).GetComponent<Image>().enabled = false;
            prevObj = null;
        }

        // 다른 오브젝트일때 오브젝트 위치랑 리스트 업데이트하기
    }

    public void SwitchItem(Transform item1, Transform item2)  // 아이템 위치 swap
    {
        // 리스트 swap
        Item tmp1 = itemList[item1.GetSiblingIndex()];
        Item tmp2 = itemList[item2.GetSiblingIndex()];
        
        itemList.RemoveAt(item1.GetSiblingIndex());
        itemList.Insert(item1.GetSiblingIndex(), tmp2);

        itemList.RemoveAt(item2.GetSiblingIndex());
        itemList.Insert(item2.GetSiblingIndex(), tmp1);

        // 오브젝트 swap
        int temp = item1.GetSiblingIndex();
        item1.SetSiblingIndex(item2.GetSiblingIndex());
        item2.SetSiblingIndex(temp);
        
        CheckItem();
    }

    public void CheckItem()
    {
        foreach (Item temp in itemList)
        {
            Debug.Log(temp.ItemName);
        }
        Debug.Log("Done");
    }
}
