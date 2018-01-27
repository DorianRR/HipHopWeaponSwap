using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

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

        if (Input.GetAxis("RightStickX_P2") > 0 || Input.GetAxis("RightStickY_P2") > 0 || Input.GetAxis("RightStickX_P2") < 0 || Input.GetAxis("RightStickY_P2") < 0)
        {
            direction = new Vector3(Input.GetAxis("RightStickX_P2"), 0, Input.GetAxis("RightStickY_P2"));
            direction.Normalize();
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
        direction = new Vector3(Input.GetAxis("RightStickX_P2"), 0, Input.GetAxis("RightStickY_P2"));
        direction.Normalize();

        if (Input.GetAxis("Horizontal_P2") > 0 || Input.GetAxis("Horizontal_P2") < 0 || Input.GetAxis("Vertical_P2") < 0 || Input.GetAxis("Vertical_P2") < 0)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(Input.GetAxis("Horizontal_P2"), 0, 0));
            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(0, 0, Input.GetAxis("Vertical_P2")));

        }
        else
        {

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


        }

        if (Input.GetAxisRaw("3rd axis P_2") > 0)
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
