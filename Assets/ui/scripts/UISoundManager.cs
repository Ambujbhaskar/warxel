using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UISoundManager : MonoBehaviour
{
    [Serializable]
    public struct NameClipZip
    {
        public string name;
        public AudioClip clip;
    }
    public NameClipZip[] audioClips;

    private AudioSource m_audioSource;
    private Dictionary<string, AudioClip> m_audioClips;

    void Start()
    {
        m_audioClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < audioClips.Length; i++)
            m_audioClips.Add(audioClips[i].name, audioClips[i].clip);

        m_audioSource = GetComponent<AudioSource>();
        if (m_audioSource == null) {
            Debug.LogError("[UISoundManager] : GameObject does not have an AudioSource!");
        }
    }

    public void Play(string name) {
        if (m_audioClips.ContainsKey(name))
        {
            m_audioSource.PlayOneShot(m_audioClips[name]);
        }
        else {
            Debug.LogError("[UISoundManager] : Request to play non existent audi clip '" + name + "'");
        }
    }
}
