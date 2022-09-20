using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;
    [SerializeField] private bool _destroyOnCollision;
    
    private void OnCollisionEnter(Collision collision)
    {
        _colAction.Action(collision.gameObject);

        if (_destroyOnCollision)
        {
            Destroy(this.gameObject); // Destroy bullet on collision
        }
    }
}
