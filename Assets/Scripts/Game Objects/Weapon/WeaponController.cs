using UnityEngine;

// bullet movement
[RequireComponent(typeof(Rigidbody2D))]
public class WeaponController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private new Rigidbody2D rigidbody;

    private Vector3 direction;


    // So that the player flying towards the bullet does not harm himself
    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void Update()
    {
        MoveBullet();
        DeleteWhenOutsideTheCamera();
    }

    private void MoveBullet()
    {
        Vector3 position = transform.position;
        position += direction * bulletSpeed * Time.deltaTime;
        rigidbody.MovePosition(position);
    }

    private void DeleteWhenOutsideTheCamera()
    {
        if(Mathf.Abs(transform.position.x) > 25.0f || Mathf.Abs(transform.position.y) > 15.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirectionToBullet(Vector2 direction)
    {
        this.direction = direction;
    }
}
