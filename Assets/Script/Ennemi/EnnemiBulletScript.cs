using UnityEngine;

public class EnnemiBulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private AnimationCurve wave;

    private int bulletType;
    private float timer = 0f;
    private Vector3 direction;
    private Vector3 initialPosition;

    public void InitializeBullet(int _bulletType, Vector3 _direction)
    {
        direction = _direction;
        bulletType = _bulletType;
        initialPosition = transform.position;
    }

    void Update()
    {
        switch(bulletType)
        {
            case 0:
                timer += Time.deltaTime * frequency;

                transform.position = new Vector3(transform.position.x, initialPosition.y + (wave.Evaluate(Time.timeSinceLevelLoad) * frequency), initialPosition.z);
                transform.position += -Vector3.right * speed * Time.deltaTime;
                break;

           case 1:
                transform.position += direction * speed * Time.deltaTime;
                break;
        }

        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !other.gameObject.GetComponent<playerScript>().GetIsInvicible())
        {
            Destroy(gameObject);
        }
    }
}
