using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Projectile projectile;

    private Vector3 direction;
    private Quaternion rotation;


    // Use this for initialization
    void Start ()
    {
        direction = new Vector3(1, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetAxis("RightStickX_P1") > 0 || Input.GetAxis("RightStickY_P1") > 0 || Input.GetAxis("RightStickX_P1") < 0 || Input.GetAxis("RightStickY_P1") < 0)
        {
            direction = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
            direction.Normalize();
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

        direction = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
        direction.Normalize();
        gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(Input.GetAxis("Horizontal_P1"), 0, 0));
        gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(0, 0, Input.GetAxis("Vertical_P1")));



        if (Input.GetAxisRaw("3rd axis P_1") > 0)
        {
            Shoot(direction);
        }
    }

    void Shoot(Vector3 shootDir)
    {
        Projectile newProjectile = Instantiate(projectile, (transform.position + (5* shootDir)), Quaternion.identity).GetComponent<Projectile>();
        newProjectile.setDirection(shootDir);
    }

}
