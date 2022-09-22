using System.Collections.Generic;
using UnityEngine;


public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;
    [SerializeField] private bool _destroyOnCollision;
    [Tooltip("If empty, a collision action will trigger with anything that collides. If you're making a pickup you probably want to put the Players tag in here (otherwise pickups can be shot)")]
    [SerializeField] private List<string> _canCollideWithTags;
    
    private void OnCollisionEnter(Collision collision)
    {
        // If tags are specified, only allow to collide with those
        // (Useful so you cant shoot a pickup etc.)
        if (_canCollideWithTags.Count > 0)
        {
            if (!_canCollideWithTags.Contains(collision.gameObject.tag))
            {
                return;
            }
        }
        
        _colAction.Action(collision.gameObject);

        if (_destroyOnCollision)
        {
            Destroy(this.gameObject);
        }
    }
}
