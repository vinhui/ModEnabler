using UnityEngine;

/// <summary>
/// Very simple script to rotate an object
/// </summary>
public class CameraRotate : MonoBehaviour
{
    public float speed = .5f;

    private void Update()
    {
        this.transform.Rotate(0, speed, 0);
    }
}