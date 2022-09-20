using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(Health))]
public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private float _shrinkSpeed;
    public UnityEvent animationDone;
    
    public void TriggerDeathAnimation()
    {
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim()
    {
        while (transform.localScale.x >= 0.01f)
        {
            transform.localScale -= Vector3.one * _shrinkSpeed;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        animationDone.Invoke();
    }
}
