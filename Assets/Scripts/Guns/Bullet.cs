using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector3 moveDirection;
    float bulletSpeed;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //GetComponent<Rigidbody>().MovePosition(moveDirection * bulletSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider hit)
    {
        //Destroy(this);
        
        if (hit.gameObject.GetComponent<Unit>())
        {
            //hit.gameObject.GetComponent<EnemyFootUnit>().AdjustHP(-1);
            //Destroy(gameObject);
        }
    } 

    void OnCollisionEnter(Collision col)
    {
        int x = 0;
    }

    public void SetData(Vector3 direction, float speed)
    {
        moveDirection = direction;
        bulletSpeed = speed;
    }
}
