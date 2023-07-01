using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWinCondition : MonoBehaviour
{

    private Transform ParentTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        ParentTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDummies();
    }

    private void checkDummies()
    {
        int dummyCount = 0;
        foreach (Transform child in ParentTransform)
        {
            if (child.tag == "Dummy")
            {
                dummyCount++;
            }
        }

        if (dummyCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
