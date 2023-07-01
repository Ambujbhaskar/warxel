using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxObjectHealth = 100;
    public GameObject destroyedObject;
    private int currentHealth;
    
    void Start()
    {
        currentHealth = maxObjectHealth;
    }

    public void TakeHit(int damage) {
        Debug.Log(gameObject.name + "Taking damage of: " + damage);
        if (currentHealth <= 0)
        {
            TakeApart();
        }
        else currentHealth -= damage;
    }

    private void TakeApart() {
        Vector3 adjustment = new Vector3(0f, -0.6f, 0f);
        Instantiate(destroyedObject, transform.position + adjustment, transform.rotation);
        Destroy(gameObject);
    }
}
