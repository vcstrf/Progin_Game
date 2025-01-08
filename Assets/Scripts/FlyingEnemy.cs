using System.Collections;
using UnityEngine;
using TMPro;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool shouldChase = false;
    [SerializeField] private Transform startingPoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (shouldChase)
        {
            Chase();
        }
        else
        {
            ReturnToStart();
        }

        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void ReturnToStart()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}