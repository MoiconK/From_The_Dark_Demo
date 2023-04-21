using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwakeningBar : MonoBehaviour
{
    //Obtener los stats del jugador
    private GameObject player;
    private Transform core;
    private Transform stats;
    protected Stats playerStats;

    //Barra de vida
    private float currentAwakening;
    [SerializeField] private Image awakeningBarFill;


    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        core = player.transform.Find("Core");
        stats = core.transform.Find("Stats");
        playerStats = stats.GetComponent<Stats>();
        currentAwakening = playerStats.currentAwakening;
        awakeningBarFill.fillAmount = currentAwakening;
    }

    public void UpdateAwakening()
    {
        currentAwakening = playerStats.currentAwakening;
        awakeningBarFill.fillAmount = currentAwakening / playerStats.maxAwakening;
        Debug.Log(currentAwakening);
    }

    public void OnEnable()
    {
        playerStats.OnAwakeningChange += UpdateAwakening;
    }
}
