using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public UIBarManager healthUI;
    public UIBarManager staminaUI;
    public UITextManager shieldUI;
    // general attributes
    [Header("General Attributes")]
    [SerializeField] private int maxHP = 100;   // max health
    [SerializeField] private int maxSP = 100;   // max stamina
    [SerializeField] private int baseArmour = 40;   // armour

    [Header("Variables")]
    [SerializeField] private float baseStaminaMultiplier = 0.15f;

    // references
    private Animator playerAnim;

    // private variables
    private int currentHP;
    private float currentSP;
    private int currentArmour;
    private float staminaMultiplier;
    public bool isDead;
    public bool isVictorious;

    void Start()
    {
        currentHP = 30;
        currentSP = (float)maxSP;
        currentArmour = baseArmour;
        
        // healthUI.setValue(currentHP);
        // staminaUI.setValue(currentSP);
        // shieldUI.setValue(currentArmour);
        
        staminaMultiplier = baseStaminaMultiplier;
        playerAnim = GetComponent<Animator>();
        isDead = false;
        isVictorious = false;
    }

    void FixedUpdate()
    {
        RegainStamina();
    }

    public void TakeHit(int damage)
    {
        int actualDamage = (int)(damage * ((float)(100 - currentArmour) / 100f));
    
        if (currentHP > 0) {
            // hit taking animation (maybe)
            // hit taking sound
            currentHP -= actualDamage;
        }

        if (currentHP <= 0)
        {
            playerAnim.SetTrigger("Death");
            isDead = true;
            
            GetComponent<PlayerCombat>().weapon.GetComponent<CapsuleCollider>().isTrigger = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            Debug.Log("Player"+currentArmour+" DEAD!");
            // die sound
            // trigger dying screen
        }
        healthUI.setValue(currentHP);
        Debug.Log(gameObject.name+" taking damage, HP = " + currentHP + "   Took actualDmg of "+actualDamage);
    }

    public void ConsumeStamina(int consumption)
    {
        if (currentSP <= 0)
        {
            currentSP = 0;
        }
        else
        {
            currentSP -= consumption;
        }
    }

    private void RegainStamina()
    {
        if (currentSP + staminaMultiplier < maxSP)
        {
            currentSP += staminaMultiplier;
        }
        else
        {
            currentSP = maxSP;
        }
        staminaUI.setValue(currentSP);
    }

    public void IncreaseHealth(int hp)
    {
        if (currentHP + hp <= maxHP)
            currentHP += hp;
        else
        {
            currentHP = maxHP;
        }
        healthUI.setValue(currentHP);
    }

    // stamina rate increase
    public void IncreaseStamina(int value/* times stamina recharge rate increases */, int seconds)
    {
        staminaMultiplier = (float)(staminaMultiplier * value);
        Invoke(nameof(ResetStaminaMultiplier), seconds);
    }
    void ResetStaminaMultiplier()
    {
        staminaMultiplier = baseStaminaMultiplier;
    }

    // armour increase
    public void IncreaseArmour(int value/* amount armour increases */, int seconds)
    {
        if (currentArmour + value <= 100)
        {
            currentArmour += value;
            shieldUI.setValue(currentArmour);
        }
        Invoke(nameof(ResetArmourValue), seconds);
    }
    void ResetArmourValue()
    {
        currentArmour = baseArmour;
    }

    // getters
    public int GetStaminaValue()
    {
        return (int)currentSP;
    }

    public int GetArmourValue()
    {
        return currentArmour;
    }
    public int GetHealthValue()
    {
        return currentHP;
    }
}
