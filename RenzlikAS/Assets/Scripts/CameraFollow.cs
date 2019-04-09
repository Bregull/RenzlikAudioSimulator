using UnityEngine;

public class CameraFollow : MonoBehaviour   
{
    private Camera myCamera ; 
    public Transform cameraAudioSource;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = cameraAudioSource.position+offset;
    }
    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }
    
}
