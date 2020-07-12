using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraPos;
    public float scrollSpeed;

    public float topBarrier;
    public float botBarrier;
    public float leftBarrier;
    public float rightBarrier;

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y >= Screen.height * topBarrier) {
            cameraPos.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.y <= Screen.height * botBarrier) {
            cameraPos.Translate(-Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width * rightBarrier) {
            cameraPos.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.x <= Screen.height * leftBarrier) {
            cameraPos.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);
        }
    }
}
