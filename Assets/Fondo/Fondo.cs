using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    [SerializeField] private Vector2 tillingSpeed;

    private Vector2 offset;

    private Material material;

    private Rigidbody2D rigidbodyplayer;
    
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rigidbodyplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        offset = ((rigidbodyplayer.velocity.x * 0.1f) * tillingSpeed * Time.deltaTime);
        material.mainTextureOffset += offset;
    }
}
