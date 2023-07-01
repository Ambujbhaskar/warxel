using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public int steps = 10;
    public UISoundManager soundManager;
    public UnityEvent onClick;

    [HideInInspector] public bool isClicked;
    [HideInInspector] public bool isHovered;

    private float idleHeight;

    void Start()
    {
        isClicked = false;
        isHovered = false;
        idleHeight = transform.position.y;
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        soundManager.Play("hover");
        StopAllCoroutines();
        StartCoroutine(smoothTransition(idleHeight / 2));
    }
    private void OnMouseExit()
    {
        isHovered = false;
        isClicked = false;
        StopAllCoroutines();
        StartCoroutine(smoothTransition(idleHeight));
    }
    private void OnMouseDown()
    {
        isClicked = true;
        soundManager.Play("click");
        StopAllCoroutines();
        StartCoroutine(smoothTransition(0));
    }
    private void OnMouseUp()
    {
        isClicked = false;
        StopAllCoroutines();
        StartCoroutine(smoothTransition(idleHeight));
    }
    private void OnMouseUpAsButton()
    {
        isClicked = false;
        if (onClick != null)
            onClick.Invoke();
        StopAllCoroutines();
        StartCoroutine(smoothTransition(idleHeight));
    }
    IEnumerator smoothTransition(float targetHeight)
    {
        for (int i = 0; i < steps; i++)
        {
            transform.Translate(new Vector3(0.0f, targetHeight - transform.position.y, 0.0f) / steps, Space.World);
            yield return null;
        }
    }
}
