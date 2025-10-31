using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNode : MonoBehaviour
{
    private float rot;

    void Awake()
    {
        rot = GetComponent<Transform>().rotation.z;
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, rot - 60.0f));
        rot %= 360.0f;
        rot -= 60.0f;
    }
}
