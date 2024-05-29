using Unity.VisualScripting;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 TargetPosition  = playerTransform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPosition  =new Vector3(playerTransform.position.x, offset.y+playerTransform.position.y/5) + offset;
        transform.position = Vector3.SmoothDamp(transform.position,TargetPosition,ref velocity,smoothTime);
    }
}
