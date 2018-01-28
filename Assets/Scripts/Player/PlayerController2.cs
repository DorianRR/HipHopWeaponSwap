using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour {

    public Projectile projectile;
    public Slider healthSlider;
    public GameObject player;

    public bool isYelling = false;
    public bool hasLiquor;
    public bool isShotgun;
    public float moveSpeed = 15f;
    public Colour weaponColor;
    public bool swapLiquor = false;
    public bool swapWeapon = false;



    private Vector3 direction;
    private Quaternion rotation;
    private float health;
    private bool canShoot = true;
    private float timeRest = 2f;
    private float time = 0;

    // Use this for initialization
    void Start ()
    {
        direction = new Vector3(0, 0, 1);
        health = 100f;
    }

    // Update is called once per frame
    void Update ()
    {
        time += Time.deltaTime;
        if (time >= timeRest)
        {
            canShoot = true;
            time = 0;
        }
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
            if (canShoot)
            {
                Shoot(direction, isShotgun);
                canShoot = false;
            }
        }
        if (Input.GetButtonDown("Yell_P2"))
        {
            StartCoroutine(Yell());
        }
        health -= .05f;
        healthSlider.value = health;
        if (Input.GetButtonDown("SwapLiquor_P1"))
        {
            //StartCoroutine(swapLiquor());
            swapLiquor = true;
        }
        if (Input.GetButtonUp("SwapLiquor_P1"))
        {
            swapLiquor = false;
        }
        if (Input.GetButtonDown("SwapWeapon_P1"))
        {
            swapWeapon = true;
            //StartCoroutine(swapWeapon());
        }
        if (Input.GetButtonUp("SwapWeapon_P1"))
        {
            swapWeapon = false;
        }
        if (player.GetComponent<PlayerController2>().swapLiquor)
        {
            hasLiquor = (!hasLiquor);
        }

        if (player.GetComponent<PlayerController2>().swapWeapon)
        {
            isShotgun = (!isShotgun);
        }

    }

    void Shoot(Vector3 shootDir, bool isShotgun)
    {
        if (!isShotgun)
        {
            StartCoroutine(Burst(shootDir));
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

    IEnumerator Burst(Vector3 shootDir)
    {
        for (int i = 0; i < 6; i++)
        {
            Projectile newProjectile = Instantiate(projectile, (transform.position + (5 * shootDir)), Quaternion.identity).GetComponent<Projectile>();
            newProjectile.setDirection(shootDir);
            newProjectile.setColour(weaponColor);
            yield return new WaitForSeconds(.15f);
        }
    }
}
