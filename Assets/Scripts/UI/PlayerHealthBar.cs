using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //Obtener los stats del jugador
    private GameObject player;
    private Transform core;
    private Transform stats;
    protected Stats playerStats;

    //Barra de vida
    private float currentHealth;
    [SerializeField] private Image healthBarFill;
    

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        core = player.transform.Find("Core");
        stats = core.transform.Find("Stats");
        playerStats = stats.GetComponent<Stats>();
        currentHealth = playerStats.maxHealth;
        healthBarFill.fillAmount = currentHealth;
    }

    public void UpdateHealth()
    {
        currentHealth = playerStats.currentHealth;
        healthBarFill.fillAmount = currentHealth / playerStats.maxHealth;
    }

    public void OnEnable()
    {
        playerStats.OnHealthChange += UpdateHealth;
    }
}
