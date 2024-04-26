using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Image healthBar;

    public float maxHealth;
    public float currentHealth;

    public float maxWeaponEnergy;
    public float currentWeaponEnergy;

    public int money;

    private void Start()
    {
       UpdateHealthBar();
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        UpdateHealthBar();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void GetMoney(int moneyAmount)
    {
        money += moneyAmount;
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        //TODO: Game over logic
    }
}
