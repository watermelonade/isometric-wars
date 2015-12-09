using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public float bulletSpeed = 30;
    public int magSize = 4;

    int shotsFired = 0;

	public bool Fire(Vector3 aim)
    {
        shotsFired++;
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Shot", typeof(GameObject))) as GameObject;

        bullet.GetComponent<Rigidbody>().transform.position = gameObject.transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(aim);
        bullet.GetComponent<Rigidbody>().velocity = (aim - bullet.transform.position).normalized * bulletSpeed;
        
        if(shotsFired < magSize)
        {
            return true;
        } else
        {
            return false;
        }
        
       
    }
}
