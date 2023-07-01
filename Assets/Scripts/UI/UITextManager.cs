using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextManager : MonoBehaviour
{
    public int maxValue = 100;
    private TextMeshProUGUI tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponentInChildren<TextMeshProUGUI>();
        tm.text = maxValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(int value) {
        tm.text = value.ToString();
    }
}
