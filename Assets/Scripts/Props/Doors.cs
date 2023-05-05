using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private int enemiesLeft;
    private int enemigos;

    private void Start()
    {
        enemigos = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update()
    {         
        destroyDoor();
        enemigos = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(enemigos);
    }

    private void destroyDoor()
    {
        if (enemigos == enemiesLeft)
        {
            Debug.Log(enemigos);
            Destroy(gameObject);
        }
    }
}
