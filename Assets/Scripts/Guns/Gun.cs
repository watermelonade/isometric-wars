using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    float bulletSpeed = 10;


	public void Fire(Vector3 aim)
    {
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet",typeof(GameObject))) as GameObject;

        bullet.GetComponent<Rigidbody>().transform.position = gameObject.transform.position;

        //bullet.GetComponent<Rigidbody>().AddForce((aim - bullet.transform.position).normalized * bulletSpeed);
        //Vector3 tmp = aim - bullet.transform.position;
        //tmp.Normalize();
        //tmp = tmp * bulletSpeed;
        bullet.GetComponent<Rigidbody>().velocity = (aim - bullet.transform.position).normalized * bulletSpeed;
        //Vector3 direction = (aim - bullet.transform.position).normalized;

        //bullet.GetComponent<Bullet>().SetData(direction,bulletSpeed);


    }
}
