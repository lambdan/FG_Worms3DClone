using System.Collections.Generic;
using UnityEngine;


public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;
    [SerializeField] private bool _destroyOnCollision;
    [SerializeField] private List<string> _validColliderTags;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_validColliderTags.Count > 0)
        {
            if (!_validColliderTags.Contains(collision.gameObject.tag))
            {
                return;
            }
        }
        
        _colAction.Action(collision.gameObject);

        if (_destroyOnCollision)
        {
            
            
            Destroy(this.gameObject); // Destroy bullet on collision
        }
    }
}
