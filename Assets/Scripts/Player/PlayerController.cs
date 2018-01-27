using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Projectile projectile;
    //public GameObject projectile;
    //public GameObject shootPos;

    // Use this for initialization
    void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + new Vector3(Input.GetAxis("Horizontal_P1"), 0, 0);
        gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + new Vector3(0, 0, Input.GetAxis("Vertical_P1"));

        //if(Input.GetAxis("RightStickY") > 0)
        //{
        //    Debug.Log("Success!");
        //}
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 shootDir = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
            shootDir.Normalize();
            Shoot(shootDir);
        }

    }

    void Shoot(Vector3 shootDir)
    {
        Debug.Log("Fire!!");

        //Vector3 shootDirection = new Vector3(1, 0, 0);
        Projectile newProjectile = Instantiate(projectile, (transform.position + (10* shootDir)), Quaternion.identity).GetComponent<Projectile>();

        newProjectile.setDirection(shootDir);

        //GameObject bullet = Instantiate(projectile, shootPos.transform.position, Quaternion.identity) as GameObject;
        //Instantiate(projectile);
        
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
    }

    void fire(Vector3 fireDirection)
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.setDirection(fireDirection);
    }
}
