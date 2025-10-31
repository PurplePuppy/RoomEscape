using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NodeCtrl : MonoBehaviour
{
    private int nodeCount = 26;     // 총 노드 개수
    private Transform[,] nodeAry = new Transform[4, 7];

    void Awake()
    {       
        for(int i = 0; i < nodeCount; i++)
        {
            

            transform.GetChild(i).AddComponent<RotateNode>();

            // 오브젝트 별 회전 이벤트 처리
            transform.GetChild(i).AddComponent<Button>().onClick.AddListener
            (transform.GetChild(i).GetComponent<RotateNode>().Rotate);
        }
        
    }

    void Start()
    {
        // 첫번째와 마지막 노드 회전 불가
        transform.GetChild(0).GetComponent<Button>().enabled = false;
        transform.GetChild(nodeCount - 1).GetComponent<Button>().enabled = false;

        int rowCount = 0;
        int colCount = 0;

        foreach (Transform temp in GetComponentInChildren<Transform>())
        {
            Debug.Log(temp.name);   // 자손 오브젝트까지 안가는듯
            if (temp.name.Contains("Wire"))
            {
                Debug.Log(temp);
                nodeAry[rowCount, colCount++] = temp;
            }

            if (rowCount % 2 == 0 && colCount == 6)
            {
                rowCount++;
            }

            else if (rowCount % 2 != 0 && colCount == 5)
            {
                rowCount++;
            }
        }

        // Debug.Log(nodeAry[0, 0].name);
        // Debug.Log(nodeAry[0, 1].name);
        // Debug.Log(nodeAry[0, 2].name);
        // Debug.Log(nodeAry[0, 3].name);
        // Debug.Log(nodeAry[0, 4].name);
    }
}
