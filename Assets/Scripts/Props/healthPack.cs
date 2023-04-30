using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    public Stats stats;
    [SerializeField] private float healingAmount = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stats.IncreaseHealth(healingAmount);
            this.enabled = false;
        }
    }
}
