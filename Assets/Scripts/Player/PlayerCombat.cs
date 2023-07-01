using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Weapon Reference")]
    [Tooltip("Add the weapon reference of the respective character")]
    [SerializeField] public GameObject weapon;

    [Header("Attack damages")]
    public int attack1Damage = 20;
    public int attack2Damage = 50;

    [Header("Attack Stamina Consumptions")]
    public int attack1Stamina = 20;
    public int attack2Stamina = 50;
    private int attackType = 0; // no attack
    public bool attack2 = false;

    public int getAttackDamage()
    {
        switch (attackType)
        {
            case 1:
                return attack1Damage;
            case 2:
                return attack2Damage;
            default:
                return 0;
        }
    }
    public int getAttackStamina()
    {
        switch (attackType)
        {
            case 1:
                return attack1Stamina;
            case 2:
                return attack2Stamina;
            default:
                return 0;
        }
    }

    private Animator playerAnim;
    private PlayerLife playerLife;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerLife = GetComponent<PlayerLife>();
    }

    void Update()
    {
        handleAttackInput();
        // Debug.Log("Attacktype: " + attackType);
    }

    private void handleAttackInput()
    {
        if (
            Input.GetMouseButtonDown(0)
            && !playerAnim.GetBool("roll")
            && !playerAnim.GetBool("jump")
            && !playerAnim.GetBool("Attack2")
            && !playerAnim.GetBool("Attack1")
            && !playerLife.isDead
            )
        {
            attackType = 1;
            if (playerLife.GetStaminaValue() >= getAttackStamina())
            {
                weapon.GetComponent<CapsuleCollider>().enabled = true;
                playerAnim.SetBool("Attack1", true);
            }
            else
            {
                attackType = 0;
            }
            playerLife.ConsumeStamina(getAttackStamina());
        }
        if (
            Input.GetMouseButtonDown(1)
            && !playerAnim.GetBool("roll")
            && !playerAnim.GetBool("jump")
            && !playerAnim.GetBool("Attack1")
            && !playerAnim.GetBool("Attack2")
            && (playerAnim.GetInteger("state") == 1)                    // orc specific
            && !playerLife.isDead
            )
        {
            attackType = 2;
            if (playerLife.GetStaminaValue() >= getAttackStamina())
            {
                weapon.GetComponent<CapsuleCollider>().enabled = true;
                playerAnim.SetBool("Attack2", true);
                toggleAttack2Movement();                                // orc specific
            }
            else
            {
                attackType = 0;
            }
            playerLife.ConsumeStamina(getAttackStamina());
        }
    }

    private void attack1Over()
    {
        weapon.GetComponent<CapsuleCollider>().enabled = false;
        playerAnim.SetBool("Attack1", false);
        attackType = 0;
        weapon.GetComponent<Weapon>().setAttack();
    }

    private void toggleAttack2Movement()
    {
        attack2 = !attack2;
    }
    private void attack2Over()
    {
        weapon.GetComponent<CapsuleCollider>().enabled = false;
        playerAnim.SetBool("Attack2", false);
        attackType = 0;
        weapon.GetComponent<Weapon>().setAttack();
    }
}
