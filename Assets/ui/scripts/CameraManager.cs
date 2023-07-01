using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * No camera should have an AudioListener.
 * The script automatically adds an AudioListener to the current camera
 * *
     */
public class CameraManager : MonoBehaviour
{
    public string defaultCamera;
    [Serializable]
    public struct NameCameraZip
    {
        public string name;
        public Camera camera;
    }
    public NameCameraZip[] cameras;

    private Camera m_currentCamera;
    private Dictionary<string, Camera> m_cameras;

    void Start()
    {
        m_cameras = new Dictionary<string, Camera>();
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].camera.enabled = false;
            m_cameras.Add(cameras[i].name, cameras[i].camera);
        }
    }

    public void ChangeCamera(string name)
    {
        if (m_cameras.ContainsKey(name))
        {
            if (m_currentCamera != null)
            {
                m_currentCamera.enabled = false;
                Destroy(m_currentCamera.GetComponent<AudioListener>());
            }
            m_currentCamera = m_cameras[name];
            m_currentCamera.enabled = true;
            m_currentCamera.gameObject.AddComponent(typeof(AudioListener));
        }
        else
        {
            Debug.LogError("[CameraManager] : Request to switch camera to non existent camera '" + name + "'");
        }
    }
}
