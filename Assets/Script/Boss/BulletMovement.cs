using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;

    private float verticalSpeed = 10f;

    void Update()
    {
        // Move the bullet based on its local up direction
        transform.Translate(-transform.right * speed * Time.deltaTime, Space.World);

        // Check if the bullet is outside the screen and destroy it
        if (IsOutsideScreen())
        {
            Destroy(gameObject);
        }
    }

    // Set the vertical speed directly
    public void SetVerticalSpeed(float verticalOffset)
    {
        verticalSpeed = verticalOffset;
    }

    bool IsOutsideScreen()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x < 0f;
    }



}
