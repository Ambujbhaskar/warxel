using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private Popup myPopup;
    [SerializeField] private string text;
    bool escPressed;
    bool active = false;
    private void OnTriggerEnter(Collider other) {
        active = true;
        myPopup.gameObject.SetActive(true);
        Time.timeScale = 0;
        myPopup.messageText.text = text;
    }

    private void Update() {
        escPressed = Input.GetKey(KeyCode.Escape);
        if (escPressed && active) {
            closePopup();
        }
    }

    private void closePopup() {
        myPopup.gameObject.SetActive(false);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
