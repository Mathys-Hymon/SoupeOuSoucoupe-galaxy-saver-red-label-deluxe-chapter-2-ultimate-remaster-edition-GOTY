using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Move the bullet to the left continuously
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
