using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadScreenManager : IScreenManager
{
    public float delay = 2.0f;

    public Vector3 characterLocation;
    public Quaternion characterRotation;
    public float characterScale;

    public Color[] colors;
    public GameObject[] characters;

    public Material loadScreenMaterial;

    public void LoadScene(string name)
    {
        int sceneId = 1;
        if (name == "tutorial")
            sceneId = 2;
        if (name == "play")
            sceneId = 3;

        StartCoroutine(LoadSceneAsync(sceneId));
    }

    private IEnumerator LoadSceneAsync(int sceneId)
    {
        SpawnCharacter(characters[UnityEngine.Random.Range(0, characters.Length)]);
        loadScreenMaterial.color = colors[UnityEngine.Random.Range(0, colors.Length)];
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneId);
    }

    private void SpawnCharacter(GameObject model)
    {
        GameObject character = Instantiate(model, characterLocation, characterRotation);
        character.transform.localScale = Vector3.one * characterScale;
    }

    override
    public void Enable() {
        LoadScene(GameState.requestedGameMode);
    }
    override
    public void Disable() {
    }
}
