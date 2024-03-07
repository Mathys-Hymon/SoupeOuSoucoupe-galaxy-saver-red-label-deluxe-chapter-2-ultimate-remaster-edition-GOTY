using UnityEngine;

public class EnnemiBulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private AnimationCurve wave;

    private int bulletType;
    private float timer = 0f;
    private Vector3 direction;

    public void InitializeBullet(int _bulletType, Vector3 _direction)
    {
        direction = _direction;
        bulletType = _bulletType;
    }

    void Update()
    {
        switch(bulletType)
        {
            case 0:
                timer += Time.deltaTime * frequency;

                transform.localPosition = new Vector3(transform.localPosition.x, wave.Evaluate(timer) * frequency, 0);
                transform.localPosition += -Vector3.right * speed * Time.deltaTime;
                break;

           case 1:
                transform.localPosition += direction * speed * Time.deltaTime;
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
