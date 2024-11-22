using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    
    public event Action onBattle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    public void Update()
    {
        Move();
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        Move();
    }

    void Move()
    {
        movementDirection = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }

    void Battle()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colliding");
        if (other.tag == "enemy")
        {
            Debug.Log("Bing");
            SceneChanger.instance.ChangeScene("BattleTest");
        }
        Destroy(other.gameObject);
    }
}
