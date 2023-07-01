using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectScreenManager : IScreenManager
{
    public Vector3 characterLocation;
    public Quaternion characterRotation;
    public float characterScale;
    public ScreenManager screenManager;
    [Serializable]
    public struct NameCharacterZip
    {
        public string name;
        public GameObject model;
    }
    public NameCharacterZip[] characters;

    private WaveManager wm;
    private bool isEnabled;
    private KeyValuePair<int, GameObject> currentCharacter;

    private void Start()
    {
        isEnabled = false;
        wm = GetComponent<WaveManager>();
        if (characters.Length == 0)
            Debug.LogError("[SelectScreenManager] : character list empty");
        currentCharacter = new KeyValuePair<int, GameObject>(0, SpawnCharacter(characters[0].model));
    }

    private void Update()
    {
        if (!isEnabled) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToHome();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            CycleCharacter(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            CycleCharacter(-1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleConfirm();
        }
    }

    private GameObject SpawnCharacter(GameObject model)
    {
        GameObject character = Instantiate(model, characterLocation, characterRotation);
        character.transform.localScale = Vector3.one * characterScale;
        return character;
    }

    public void CycleCharacter(int delta) {
        int index = (currentCharacter.Key + delta);
        if (index < 0) index = characters.Length - 1; 
        index %= characters.Length;
        Destroy(currentCharacter.Value);
        currentCharacter = new KeyValuePair<int, GameObject>(index, SpawnCharacter(characters[index].model));
    }

    public void HandleConfirm() {
        GameState.chosenCharacter = characters[currentCharacter.Key].name;
        if (GameState.requestedGameMode == "play")
            screenManager.ChangeScreen("host");
        else if (GameState.requestedGameMode == "tutorial")
            screenManager.ChangeScreen("loading");
        else
            screenManager.ChangeScreen("home");
    }

    public void ReturnToHome()
    {
        screenManager.ChangeScreen("home");
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
