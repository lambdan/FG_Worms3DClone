using UnityEngine;

public class LevelPreviewSpinner : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * 10);
    }
}
