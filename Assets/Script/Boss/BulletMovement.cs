using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed = 6;

    void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;
    }
}
