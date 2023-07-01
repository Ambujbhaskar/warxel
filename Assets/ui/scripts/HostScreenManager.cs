using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HostScreenManager : IScreenManager
{
    public ScreenManager screenManager;

    private WaveManager wm;
    private bool isEnabled;

    private void Start()
    {
        isEnabled = false;
        wm = GetComponent<WaveManager>();
    }

    private void Update()
    {
        if (!isEnabled) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleEscape();
    }

    public void HandleEscape()
    {
        screenManager.ChangeScreen("select");
    }
    public void HandleHost()
    {
        screenManager.ChangeScreen("loading");
    }
    public void HandleJoin()
    {
        screenManager.ChangeScreen("loading");
    }

    override
    public void Enable()
    {
        wm.Enable();
        isEnabled = true;
    }
    override
    public void Disable()
    {
        wm.Disable();
        isEnabled = false;
    }
}
