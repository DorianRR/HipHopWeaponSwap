using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Projectile projectile;
    public Slider healthSlider;
    public bool isYelling = false;
    public bool hasLiquor;
    public bool isShotgun;

    private Vector3 direction;
    private Quaternion rotation;
    private float health;

    void Start ()
    {
        direction = new Vector3(1, 0, 0);
        health = 100f;
       
    }
	
	void Update ()
    {
        
        if (Input.GetAxis("RightStickX_P1") > 0 || Input.GetAxis("RightStickY_P1") > 0 || Input.GetAxis("RightStickX_P1") < 0 || Input.GetAxis("RightStickY_P1") < 0)
        {
            direction = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
            direction.Normalize();
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

        direction = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
        direction.Normalize();
        if (Input.GetAxis("Horizontal_P1") > 0 || Input.GetAxis("Horizontal_P1") < 0 || Input.GetAxis("Vertical_P1") < 0 || Input.GetAxis("Vertical_P1") < 0)
        {
            gameObject.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(Input.GetAxis("Horizontal_P1"), 0, 0));
            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + 0.5f * (new Vector3(0, 0, Input.GetAxis("Vertical_P1")));

        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        if (Input.GetAxisRaw("3rd axis P_1") > 0)
        {
            Shoot(direction, isShotgun);
        }
        if (Input.GetButtonDown("Yell_P1"))
        {
            StartCoroutine(Yell());
            
        }
        healthSlider.value = health;
    }

    void Shoot(Vector3 shootDir, bool isShotgun)
    {
        if(!isShotgun)
        {
            Projectile newProjectile = Instantiate(projectile, (transform.position + (5* shootDir)), Quaternion.identity).GetComponent<Projectile>();
            newProjectile.setDirection(shootDir);
        }
        else
        {

        }
        
    }


    public void getHit()
    {
        health -= 24f;
    }

    IEnumerator Yell()
    {
        isYelling = true;
        yield return new WaitForSeconds(2f);
        isYelling = false;
    }

}
