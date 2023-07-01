using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenManager : MonoBehaviour
{
    public string defaultScreen;
    [Serializable]
    public struct NameManagerZip
    {
        public string name;
        public IScreenManager manager;
    }
    public NameManagerZip[] screens;

    private CameraManager cm;
    private IScreenManager m_currentScreen;
    private Dictionary<string, IScreenManager> m_screens;
    private bool isStart;

    void Start()
    {
        isStart = true;
        m_screens = new Dictionary<string, IScreenManager>();
        for (int i = 0; i < screens.Length; i++)
        {
            m_screens.Add(screens[i].name, screens[i].manager);
        }
        cm = GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (isStart)
        {
            ChangeScreen(defaultScreen);
            isStart = false;
        }
    }

    public void ChangeScreen(string name)
    {
        if (m_screens.ContainsKey(name))
        {
            if (m_currentScreen != null)
            {
                m_currentScreen.Disable();
            }
            m_currentScreen = m_screens[name];
            m_currentScreen.Enable();
            cm.ChangeCamera(name);
        }
        else
        {
            Debug.LogError("[ScreenManager] : Request to switch screen to non existent screen '" + name + "'");
        }
    }
}
