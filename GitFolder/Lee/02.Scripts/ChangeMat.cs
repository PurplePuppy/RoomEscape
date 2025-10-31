using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    public Material[] mat;

    int i = 0;


    public void MatChange()
    {

        i = ++i % 2;
        GetComponent<MeshRenderer>().material = mat[i];
    }
}
