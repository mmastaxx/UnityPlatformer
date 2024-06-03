using Unity.VisualScripting;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 offset;
    GameObject Player;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float yMovementOffset = 2;

    public void SetTargerToFollow(GameObject currentPlayer) {Player = currentPlayer; }

    void Update()
    {
        Vector3 TargetPosition  =new Vector3(Player.transform.position.x, offset.y+ Player.transform.position.y/yMovementOffset) + offset;
        transform.position = Vector3.SmoothDamp(transform.position,TargetPosition,ref velocity,smoothTime);
    }
}
