using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter(_time));
    }
    
    IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
