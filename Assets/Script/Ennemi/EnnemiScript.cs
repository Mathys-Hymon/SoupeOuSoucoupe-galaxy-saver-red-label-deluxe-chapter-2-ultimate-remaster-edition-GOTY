using UnityEngine;

public class EnnemiScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private float shootRate;
    [SerializeField] private int bulletPerShoot;
    [SerializeField] private bulletTypeEnum bulletType;

    private enum bulletTypeEnum
    {
        wavy,
        straight,
    }

    private void Start()
    {
        InvokeRepeating("shoot", 0f, 5/shootRate);
    }

    private void shoot()
    {
        for(int i = 0; i < bulletPerShoot; i++)
        {
            var bullet = Instantiate(bulletRef, -Vector3.right, Quaternion.identity);
            var bulletScript = bullet.GetComponent<EnnemiBulletScript>();

            Vector3 direction = Vector3.zero;

            if(i == 0)
            {
                direction = -Vector3.right;
            }
            else
            {
                if (i % 2 == 0)
                {
                    direction = -Vector3.right + new Vector3(0, -i* 0.5f, 0);
                }
                else
                {
                    direction = -Vector3.right + new Vector3(0, i * 0.5f, 0);
                }

            }

            bulletScript.InitializeBullet(((int)bulletType), direction);

        }
    }
}
