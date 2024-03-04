using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float shootDelay;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private Transform[] bulletSpawn;

    private Vector2 input;
    private bool isShooting;

    private float delay;
    public static playerScript instance;

    private void Start()
    {
        instance = this;
    }
    public void playerMovement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void shoot(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValueAsButton();
    }

    private void Update()
    {
        if(isShooting && delay >= shootDelay)
        {
            delay = 0;
            for(int i = 0; i < bulletSpawn.Length; i++)
            {
                var bullet = Instantiate(bulletRef);
                bullet.transform.localPosition = bulletSpawn[i].position;
                bullet.transform.parent = null;
            }
        }
        if(delay < shootDelay)
        {
            delay += Time.deltaTime;
        }

        if(Mathf.Abs(transform.position.x) >= 11.20f)
        {
            transform.position -= new Vector3(transform.position.x/10 * Time.deltaTime, 0,0);
        }

        else
        {
            transform.position += new Vector3(input.x * (movementSpeed / 1.5f) * Time.deltaTime, 0, 0);
            
        }
        if (Mathf.Abs(transform.position.y) >= 6.15f)
        {
            transform.position -= new Vector3(0, transform.position.y/10 * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(0, input.y * (movementSpeed / 1.5f) * Time.deltaTime, 0);
        }

        transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation, Quaternion.Euler(input.x * 10, 90, input.y * 30), 20 * Time.deltaTime);

    }
}
