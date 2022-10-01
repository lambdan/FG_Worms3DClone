using UnityEngine;

public class LevelPreviewSpinner : MenuSystem
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * 10);
    }
}
