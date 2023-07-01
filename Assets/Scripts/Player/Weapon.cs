using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public bool isAttackOngoing = false;
    

    private void OnTriggerEnter(Collider collision) {
        if (!isAttackOngoing) {
            GameObject obj = collision.gameObject;
            int dmg = player.GetComponent<PlayerCombat>().getAttackDamage();
            if (obj.CompareTag("Dummy")) {
                obj.GetComponent<Destructible>().TakeHit(dmg);
            } 
            else if (obj.CompareTag("Player")) {
                obj.GetComponent<PlayerLife>().TakeHit(dmg);
                isAttackOngoing = true;
                Debug.Log(player.name + "'s Weapon inside "+obj.name);
            }
        }
    }
    private void OnTriggerExit(Collider collision) {
        if (!isAttackOngoing) {
            GameObject obj = collision.gameObject;
            int dmg = player.GetComponent<PlayerCombat>().getAttackDamage();
            if (obj.CompareTag("Dummy")) {
                obj.GetComponent<Destructible>().TakeHit(dmg);
            } 
            else if (obj.CompareTag("Player")) {
                obj.GetComponent<PlayerLife>().TakeHit(dmg);
                isAttackOngoing = true;
                Debug.Log(player.name + "'s Weapon inside "+obj.name);
            }
        }
    }

    public void setAttack() {
        isAttackOngoing = false;
    }
}
