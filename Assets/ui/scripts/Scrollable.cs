using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollable : MonoBehaviour
{
    public float scrollLimit;
    public float scrollSpeed = 2.0f;
    public float tolerance = 0.0f;

    private float zInitial;
    private bool isScrollable;
    void Start()
    {
        isScrollable = false;
        zInitial = transform.position.z;
    }
    
    void Update()
    {
        if (!isScrollable)
            return;

        float scroll = scrollSpeed * Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > tolerance)
        {
            Vector3 pos = transform.position;
            pos.z = Mathf.Max(zInitial, pos.z - scroll);
            transform.position = pos;
        }
        else if (scroll < -tolerance)
        {
            Vector3 pos = transform.position;
            pos.z = Mathf.Min(scrollLimit, pos.z - scroll);
            transform.position = pos;
        }
    }

    public void Enable() {
        isScrollable = true;
    }

    public void Disable()
    {
        isScrollable = false;
    }
}
