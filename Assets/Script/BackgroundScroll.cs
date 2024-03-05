using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x - 1 * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);

        if(transform.localPosition.x < -21.3f)
        {
            transform.localPosition = new Vector3(42.6f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
