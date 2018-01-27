using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Projectile projectile;
<<<<<<< HEAD
    public Slider healthSlider;
    public bool isYelling = false;
    public bool hasLiquor;
    public bool isShotgun;
    public float moveSpeed = 15f;

=======
    public Colour weaponColour;
>>>>>>> origin/aprilHootStuff

    private Vector3 direction;
    private Quaternion rotation;
    private float health;

    void Start ()
    {
        direction = new Vector3(0, 0, 1);
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

        //direction = new Vector3(Input.GetAxis("RightStickX_P1"), 0, Input.GetAxis("RightStickY_P1"));
        //direction.Normalize();
        if (Input.GetAxis("Horizontal_P1") > 0 || Input.GetAxis("Horizontal_P1") < 0 || Input.GetAxis("Vertical_P1") < 0 || Input.GetAxis("Vertical_P1") > 0)
        {
            gameObject.GetComponent<Rigidbody>().constraints =  
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(Input.GetAxis("Horizontal_P1"), 0, 0));
            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(0, 0, Input.GetAxis("Vertical_P1")));

        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = 
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
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
<<<<<<< HEAD
        isYelling = true;
        yield return new WaitForSeconds(2f);
        isYelling = false;
=======
        Projectile newProjectile = Instantiate(projectile, (transform.position + (5* shootDir)), Quaternion.identity).GetComponent<Projectile>();
        newProjectile.setColour(weaponColour);
        newProjectile.setDirection(shootDir);
>>>>>>> origin/aprilHootStuff
    }

}
