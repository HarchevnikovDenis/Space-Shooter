using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxX, maxY;

    public float MaxX
    {
        get { return maxX; }
    }
    public float MaxY
    {
        get { return maxY; }
    }

    private new Rigidbody2D rigidbody;
    private new Transform transform;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        LookAtMousePosition();

        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        Movement(movement);
    }

    private void LookAtMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        if (transform.position.y < mousePosition.y)
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
        if(movement.x == 0.0f && movement.y == 0.0f)
        {
            return;
        }

        movement = Vector2.ClampMagnitude(movement, 1.0f);

        Vector2 position = transform.position;
        position += movement * movementSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        position.y = Mathf.Clamp(position.y, -maxY, maxY);

        rigidbody.MovePosition(position);
    }
}
