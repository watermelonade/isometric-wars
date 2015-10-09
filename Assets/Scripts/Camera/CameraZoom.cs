using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    float cameraDistanceMin = 1;
    float cameraDistanceMax = 12;
    float cameraDistance = 7;

    float scrollSpeed = 3f;

	// Update is called once per frame
	void Update () {
        cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);

        Camera.main.orthographicSize = cameraDistance;
	}
}
