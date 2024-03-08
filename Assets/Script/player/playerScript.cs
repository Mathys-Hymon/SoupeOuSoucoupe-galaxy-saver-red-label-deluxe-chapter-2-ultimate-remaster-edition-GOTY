using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float shootDelay;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private int maxLife;
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private GameObject particleRef;
    [SerializeField] private Transform[] bulletSpawn;

    private Vector2 input;
    private bool isShooting;
    private int life;
    private bool isInvicible = false;

    private float delay;
    public static playerScript instance;

    private void Start()
    {
        instance = this;

        life = maxLife;
    }
    public void playerMovement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void shoot(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValueAsButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemie" && !isInvicible)
        {
            Damaged();
        }

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
            transform.position -= new Vector3(transform.position.x/50 * Time.deltaTime, 0,0);
        }
        else
        {
            transform.position += new Vector3(input.x * (movementSpeed / 1.5f) * Time.deltaTime, 0, 0);
        }
        if (Mathf.Abs(transform.position.y) >= 6.15f)
        {
            transform.position -= new Vector3(0, transform.position.y/50 * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(0, input.y * (movementSpeed / 1.5f) * Time.deltaTime, 0);
        }
        transform.GetChild(0).localRotation = Quaternion.Slerp(transform.GetChild(0).localRotation, Quaternion.Euler(input.x * 10, 90, input.y * 30), 20 * Time.deltaTime);
    }

    public void Damaged()
    {
        life--;

        StartCoroutine("PlayerBlink");

        if(life == 2)
        {
            GameObject.Find("Life3").SetActive(false);
        }
        else if (life == 1)
        {
            GameObject.Find("Life2").SetActive(false);
        }
        else if (life == 0)
        {
            GameObject.Find("Life1").SetActive(false);
        }

        if (life < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator PlayerBlink()
    {
        yield return new WaitForSeconds(0.01f);
        isInvicible = true;

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        isInvicible = false;
    }

    public bool GetIsInvicible()
    {
        return isInvicible;
    }
}
