using Unity.VisualScripting;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 offset;
    public Transform playerTransform;
    [SerializeField] Vector3 velocity = Vector3.zero;
    float yMovementOffset = 2;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 TargetPosition  = playerTransform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPosition  =new Vector3(playerTransform.position.x, offset.y+playerTransform.position.y/yMovementOffset) + offset;
        transform.position = Vector3.SmoothDamp(transform.position,TargetPosition,ref velocity,smoothTime);
    }
}
