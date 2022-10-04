using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float shrinkFactor = 0.99f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (transform.localScale.x > 0.2f) // If we're still pretty big, create a smaller clone
            {
                transform.localScale *= shrinkFactor;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
