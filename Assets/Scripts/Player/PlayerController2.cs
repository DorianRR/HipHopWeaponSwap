using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour {

    public Projectile projectile;
    public Slider healthSlider;
    bool isYelling = false;
    public bool hasLiquor;
    public bool isShotgun;
    public float moveSpeed = 15f;


    private Vector3 direction;
    private Quaternion rotation;
    private float health;


    // Use this for initialization
    void Start ()
    {
        direction = new Vector3(0, 0, 1);
        health = 100f;
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

        //direction = new Vector3(Input.GetAxis("RightStickX_P2"), 0, Input.GetAxis("RightStickY_P2"));
        //direction.Normalize();

        if (Input.GetAxis("Horizontal_P2") > 0 || Input.GetAxis("Horizontal_P2") < 0 || Input.GetAxis("Vertical_P2") < 0 || Input.GetAxis("Vertical_P2") > 0)
        {
            gameObject.GetComponent<Rigidbody>().constraints = 
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(Input.GetAxis("Horizontal_P2"), 0, 0));
            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(0, 0, Input.GetAxis("Vertical_P2")));

        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        if (Input.GetAxisRaw("3rd axis P_2") > 0)
        {
            Shoot(direction);
        }
        healthSlider.value = health;
        if (Input.GetButtonDown("Yell_P2"))
        {
            StartCoroutine(Yell());
        }
    }

    void Shoot(Vector3 shootDir)
    {
        Projectile newProjectile = Instantiate(projectile, (transform.position + (5* shootDir)), Quaternion.identity).GetComponent<Projectile>();
        newProjectile.setDirection(shootDir);
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
