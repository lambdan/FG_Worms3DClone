using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (transform.localScale.magnitude > 5) // If we're still pretty big, create a smaller clone
            {
                GameObject a = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                a.transform.localScale = new Vector3(this.transform.localScale.x * 0.9f, this.transform.localScale.y * 0.9f,
                    this.transform.localScale.z * 0.9f);
            }
            
            Destroy(this.gameObject);
        }
    }
}
