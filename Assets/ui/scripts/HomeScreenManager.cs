using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HomeScreenManager : IScreenManager
{
    public Vector3 characterLocation;
    public Quaternion characterRotation;
    public float characterScale;
    public GameObject[] characters;

    public int tutorialSceneId;
    public int gameSceneId;
    public ScreenManager screenManager;

    private KeyValuePair<int, GameObject> currentCharacter;
    private WaveManager wm;
    void Start()
    {
        if (characters.Length == 0)
            Debug.LogError("[HomeScreenManager] : character list should have at least one character!");

        currentCharacter = new KeyValuePair<int, GameObject>(0, SpawnCharacter(characters[0]));
        wm = GetComponent<WaveManager>();
    }

    public void HandleWarxel()
    {
        ChangeCharacter();
    }

    public void HandleTutorial() {
        screenManager.ChangeScreen("select");
        GameState.requestedGameMode = "tutorial";
    }

    public void HandlePlay()
    {
        screenManager.ChangeScreen("select");
        GameState.requestedGameMode = "play";
    }

    public void HandleControls()
    {
        screenManager.ChangeScreen("controls");
    }

    public void HandleCredits() {
        screenManager.ChangeScreen("credits");
    }

    public void HandleExit() {
        Application.Quit();
    }

    public void ChangeCharacter() {
        int index = (currentCharacter.Key + 1) % characters.Length;
        Destroy(currentCharacter.Value);
        currentCharacter = new KeyValuePair<int, GameObject>(index, SpawnCharacter(characters[index]));
    }

    private GameObject SpawnCharacter(GameObject model) {
        GameObject character = Instantiate(model, characterLocation, characterRotation);
        character.transform.localScale = Vector3.one * characterScale;
        return character;
    }

    override
    public void Enable()
    {
        wm.Enable();
    }
    override
    public void Disable()
    {
        wm.Disable();
    }
}
