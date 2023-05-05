using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    private GameObject player;
    private GameObject doors;
    private Stats stats;
    [SerializeField] private float healingAmount = 20f;
    private AudioSource healingSound;

    private void Start()
    {
        doors = GameObject.Find("Doors");
        healingSound = doors.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponentInChildren<Stats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healingSound.Play();
            stats.IncreaseHealth(healingAmount);
            Destroy(gameObject);
        }
    }
}
