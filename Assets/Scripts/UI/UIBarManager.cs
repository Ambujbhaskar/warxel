using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarManager : MonoBehaviour
{
    public float maxValue = 100;
    public float startValue = 100;
    public float sizeFactor = 4.0f;
    public float padding = 2.0f;
    private RectTransform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 size = bar.sizeDelta;
        size.x = startValue * sizeFactor;
        bar.sizeDelta = size;
        size = new Vector2( maxValue * sizeFactor + padding * 2.0f, size.y + padding * 2.0f);
        GetComponent<RectTransform>().sizeDelta = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(float value) {
        if (value > maxValue)
            value = maxValue;
        else if (value < 0.0f)
            value = 0.0f;
        Vector2 size = bar.sizeDelta;
        size.x = value * sizeFactor;
        bar.sizeDelta = size;
    }
}
