using UnityEngine;

[RequireComponent(typeof(CollisionAction))]
public class DangerZone : MonoBehaviour
{
    private CollisionAction _collisionAction;
    void Awake()
    {
        _collisionAction = GetComponent<CollisionAction>();
    }
    private void OnCollisionStay(Collision collisionInfo)
    {
        _collisionAction.Action(collisionInfo.gameObject);
    }
}
