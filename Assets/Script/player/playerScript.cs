using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float shootDelay;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private GameObject bulletRef;

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
        isShooting = context.performed;
    }

    private void Update()
    {
        if(isShooting && delay >= shootDelay)
        {
            delay = 0;
            Instantiate(bulletRef);
        }

        transform.position += new Vector3(input.x * movementSpeed*Time.deltaTime, input.y * movementSpeed*Time.deltaTime, 0);
        transform.GetChild(0).localRotation =Quaternion.Slerp(transform.GetChild(0).localRotation, Quaternion.Euler(input.x* 10, 90, input.y * 30), 20*Time.deltaTime);
    }
}
