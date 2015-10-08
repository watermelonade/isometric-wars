using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    Transform target = null;
    Vector3 battlePos = new Vector3(-6f, 14f, -6f);

    Vector3 rotateDirection;

    Quaternion startRotation = Quaternion.Euler(30, 45, 30);

    public float height = 5.0f;
    
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    float rotateSpeed = 1f;
    float angleTraversed = 0f;

    bool rotating;

    // Use this for initialization
    void Start () {
        transform.position = battlePos;
        gameObject.AddComponent<CameraZoom>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (rotating)
        {
            //float currentTime = Time.deltaTime;
            //float angle = rotateSpeed * currentTime;
            if(angleTraversed >= 90)
            {
                rotating = false;
                angleTraversed = 0f;
                return;
            }

            if (target)
            {
                transform.RotateAround(target.position, rotateDirection, rotateSpeed);
            }
            else
            {
                transform.RotateAround(Vector3.zero, rotateDirection, rotateSpeed);
            }

            angleTraversed += rotateSpeed;
            
        }

        
	}

    public void Rotate(string direction)
    {
        switch(direction)
        {
            case "left" :
                rotateDirection = Vector3.up;
                break;
            case "right":
                rotateDirection = Vector3.down;
                break;
            /*case "down":
                rotateDirection = Vector3.right;
                break;*/
            default:
                break;
        }

        rotating = true;
    }
    
    public void SetTarget(Transform obj)
    {
        target = obj;
        transform.position = new Vector3(obj.position.x, transform.position.y, obj.position.z);

    }

    public void UnsetTarget()
    {
        target = null;
        transform.position = battlePos;
    }

    
    public void Move(string direction)
    {
        switch (direction)
        {
            case "left":
                transform.Translate(Vector3.left * Time.deltaTime);
                break;
            case "right":
                transform.Translate(Vector3.right * Time.deltaTime);
                break;
            case "up":
                transform.Translate(Vector3.up * Time.deltaTime);
                break;
            case "down":
                transform.Translate(Vector3.down * Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
