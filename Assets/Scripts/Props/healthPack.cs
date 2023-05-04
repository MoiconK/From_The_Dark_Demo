using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    private GameObject player;
    private Stats stats;
    [SerializeField] private float healingAmount = 20f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponentInChildren<Stats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stats.IncreaseHealth(healingAmount);
            Destroy(gameObject);
        }
    }
}
