using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;

    private float verticalSpeed = 10f;

    void Update()
    {
        // Move the bullet based on its local up direction
        transform.Translate(-transform.right * speed * Time.deltaTime, Space.World);
    }

    // Set the vertical speed directly
    public void SetVerticalSpeed(float verticalOffset)
    {
        verticalSpeed = verticalOffset;
    }



}
