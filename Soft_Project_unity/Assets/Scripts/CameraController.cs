using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 velocity = Vector3.zero;
    float currentPosX;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
    }

    public void MoveToNewRoom(Transform room)
    {
        currentPosX = room.position.x;
    }
}
