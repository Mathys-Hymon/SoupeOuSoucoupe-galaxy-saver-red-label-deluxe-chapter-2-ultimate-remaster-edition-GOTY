using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private float speed = 1;
    private void Update()
    {
        if(speed < bulletSpeed)
        {
            speed += speed * Time.deltaTime* 4;
        }

        transform.position += transform.right * speed * Time.deltaTime;

        if(transform.position.x > 14f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnnemiBulletScript>() != null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
