using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceToStop;

    private Transform player;
    private new Rigidbody2D rigidbody;
    
    private void Awake()
    {
        try
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(gameObject.name + " didn't set Player field");
        }
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }

        LookToPlayer();

        if(Vector2.Distance(transform.position, player.position) > distanceToStop)
        {
            Vector2 movement = player.position - transform.position;
            Movement(movement);
        }
    }

    private void LookToPlayer()
    {
        float angle = Vector2.Angle(Vector2.right, player.position - transform.position);
        if (transform.position.y < player.position.y)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -angle);
        }
    }

    public void Movement(Vector2 movement)
    {
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        Vector2 position = transform.position;
        position += movement * speed * Time.deltaTime;

        rigidbody.MovePosition(position);
    }
}
