using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public bool lockRotation = false;


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (!lockRotation)
            orientation.forward = viewDir.normalized;
    }
}
