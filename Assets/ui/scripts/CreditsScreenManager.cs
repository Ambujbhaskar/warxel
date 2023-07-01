using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreenManager : IScreenManager
{
    public ScreenManager screenManager;

    private WaveManager wm;
    private Scrollable sc;
    private bool isEnabled;

    private void Start()
    {
        isEnabled = false;
        wm = GetComponent<WaveManager>();
        sc = GetComponent<Scrollable>();
    }

    private void Update()
    {
        if (isEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToHome();
        }
    }

    public void ReturnToHome()
    {
        screenManager.ChangeScreen("home");
    }

    override
    public void Enable() 
    {
        wm.Enable();
        sc.Enable();
        isEnabled = true;
    }

    override
    public void Disable()
    {
        wm.Disable();
        sc.Disable();
        isEnabled = false;
    }
}
