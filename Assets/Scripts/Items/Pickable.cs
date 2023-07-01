using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject model;
    public GameObject particles;
    public Vector3 orientation;
    public Vector3 rotationUpdate;
    public float bobbingSpeed = 4.0f;
    public float bobbingFactor = 2.0f;
    public int respawnTime = 8;

    [Header("type: 1-Health, 2-Stamina, 3-Shield")]
    public int type;

    [Header("Type Values")]

    [SerializeField] int healthValue = 60;
    [SerializeField] int armourValue = 10;
    [SerializeField] int armourTime = 5;
    [SerializeField] int staminaValue = 2;
    [SerializeField] int staminaTime = 10;

    private bool depleted;
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = Instantiate(model, transform.position, transform.rotation);
        child.transform.parent = gameObject.transform;
        depleted = false;
        child.transform.Rotate(orientation);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!depleted)
        {
            child.transform.Rotate(rotationUpdate, Space.World);
            child.transform.Translate(Vector3.up * 0.005f * bobbingFactor * Mathf.Sin(bobbingSpeed * Time.time), Space.World);
        }
    }

    IEnumerator Action()
    {
        depleted = true;
        child.SetActive(false);
        particles.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        child.SetActive(true);
        particles.SetActive(true);
        depleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !depleted)
        {
            Debug.Log("Picked up by player, type:" + type);
            switch (type)
            {
                case 1:
                    other.gameObject.GetComponent<PlayerLife>().IncreaseHealth(healthValue);
                    break;
                case 2:
                    other.gameObject.GetComponent<PlayerLife>().IncreaseStamina(staminaValue, staminaTime);
                    break;
                case 3:
                    other.gameObject.GetComponent<PlayerLife>().IncreaseArmour(armourValue, armourTime);
                    break;
            }
            StartCoroutine(Action());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.white;
    }
}
