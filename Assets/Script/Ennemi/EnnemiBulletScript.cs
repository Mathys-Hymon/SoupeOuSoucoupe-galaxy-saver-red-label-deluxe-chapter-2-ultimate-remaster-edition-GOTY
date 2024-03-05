using UnityEngine;

public class EnnemiBulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private AnimationCurve wave;

    private int bulletType;
    private float timer = 0f;

    public void InitializeBullet(int _bulletType)
    {
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
           break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
