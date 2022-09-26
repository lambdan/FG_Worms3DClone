using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (transform.localScale.x > 0.1)
            {
                GameObject a = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                a.transform.localScale = new Vector3(this.transform.localScale.x * 0.9f, this.transform.localScale.y * 0.9f,
                    this.transform.localScale.z * 0.9f);
                Destroy(this.gameObject);
            }
        }
    }
}
