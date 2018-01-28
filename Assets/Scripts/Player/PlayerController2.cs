using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController2 : MonoBehaviour
{

    public Projectile projectile;
    public ShotgunProjectile shotgunProjectile;
    public Slider healthSlider;
    public GameObject otherPlayer;
    public GameObject yelling;
    public Canvas canvas;

    public bool isYelling = false;
    public bool hasLiquor;
    public bool isShotgun = true;
    public float moveSpeed = 15f;
    public Colour weaponColor;
    public bool readyToSwapLiquor = false;
    public bool readyToSwapWeapon = false;
    public float timeBetweenShotgunShots = 1f;



    private Vector3 direction;
    private Quaternion rotation;
    private float health;
    private bool canShoot = true;
    private float timeRest = 1.2f;
    private float time = 0;
    private bool shotgunCanShoot;
    private Animator anim;



    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        direction = new Vector3(0, 0, 1);
        health = 100f;
        shotgunCanShoot = true;
        yelling.SetActive(false);

    }

    void Update()
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((direction)), .30f);

        }

        if (Input.GetAxis("Horizontal_P2") > 0 || Input.GetAxis("Horizontal_P2") < 0 || Input.GetAxis("Vertical_P2") < 0 || Input.GetAxis("Vertical_P2") > 0)
        {
            anim.SetBool("isDancing", false);
            anim.SetBool("isMoving", true);
            gameObject.GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(Input.GetAxis("Horizontal_P2"), 0, 0));
            gameObject.GetComponent<Rigidbody>().position = gameObject.GetComponent<Rigidbody>().position + moveSpeed * Time.deltaTime * (new Vector3(0, 0, Input.GetAxis("Vertical_P2")));

        }
        else
        {
            anim.SetBool("isDancing", true);
            anim.SetBool("isMoving", false);
            gameObject.GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        if (Input.GetAxisRaw("3rd axis P_2") > 0)
        {
            if (canShoot || isShotgun)
            {
                anim.SetBool("isMoving", false);
                anim.SetBool("isDancing", false);
                anim.SetBool("isShooting", true);
                Shoot(isShotgun);
                canShoot = false;
            }

        }
        else
        {
            anim.SetBool("isShooting", false);

        }
        if (Input.GetButtonDown("Yell_P2"))
        {
            StartCoroutine(Yell());

        }

        if (Input.GetButtonDown("SwapLiquor_P2"))
        {
            anim.SetBool("isCatching", false);
            anim.SetBool("isDrinking", false);
            readyToSwapLiquor = true;
        }
        if (Input.GetButtonUp("SwapLiquor_P2"))
        {
            readyToSwapLiquor = false;
        }
        if (Input.GetButtonDown("SwapWeapon_P2"))
        {
            anim.SetBool("isCatching", false);
            anim.SetBool("caughtWeapon", false);
            readyToSwapWeapon = true;
            StartCoroutine(BlinkingWeapon());
        }
        if (Input.GetButtonUp("SwapWeapon_P2"))
        {
            readyToSwapWeapon = false;
        }

        if (otherPlayer.GetComponent<PlayerController>().readyToSwapLiquor && readyToSwapLiquor)
        {
            swapLiquor();

            readyToSwapLiquor = false;
            otherPlayer.GetComponent<PlayerController>().readyToSwapLiquor = false;
            otherPlayer.GetComponent<PlayerController>().swapLiquor();
        }

        if (otherPlayer.GetComponent<PlayerController>().readyToSwapWeapon && readyToSwapWeapon)
        {
            anim.SetBool("caughtWeapon", true);

            isShotgun = (!isShotgun);
            swapColour();
            otherPlayer.GetComponent<PlayerController>().readyToSwapWeapon = false;
            readyToSwapWeapon = false;
            otherPlayer.GetComponent<PlayerController>().isShotgun = !otherPlayer.GetComponent<PlayerController>().isShotgun;
            otherPlayer.GetComponent<PlayerController>().swapColour();
        }

        health -= .05f;
        healthSlider.value = health;


    }
    private void LateUpdate()
    {
        CheckDeath();

    }


    void Shoot(bool isShotgun)
    {
        if (!isShotgun)
        {
            StartCoroutine(Burst());
        }
        else
        {
            if (shotgunCanShoot)
            {
                StartCoroutine(ShootShotgun());
                ShotgunProjectile newProjectile = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile2 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile3 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile4 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile5 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile6 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();
                ShotgunProjectile newProjectile7 = Instantiate(shotgunProjectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<ShotgunProjectile>();

                newProjectile.setDirection(direction);
                newProjectile2.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));
                newProjectile3.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));
                newProjectile4.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));
                newProjectile5.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));
                newProjectile6.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));
                newProjectile7.setDirection(direction + new Vector3(Random.Range(-.25f, .25f), 0, Random.Range(-.25f, .25f)));

                newProjectile.setColour(weaponColor);
                newProjectile2.setColour(weaponColor);
                newProjectile3.setColour(weaponColor);
                newProjectile4.setColour(weaponColor);
                newProjectile5.setColour(weaponColor);
                newProjectile6.setColour(weaponColor);
                newProjectile7.setColour(weaponColor);
            }
        }

    }


    private void getHit()
    {
        health -= 10f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyProjectile>() != null)
        {
            getHit();
            Destroy(other.gameObject);
        }
    }

    IEnumerator Yell()
    {
        isYelling = true;
        yelling.SetActive(true);
        yield return new WaitForSeconds(5f);
        isYelling = false;
        yelling.SetActive(false);

    }

    IEnumerator Burst()
    {
        for (int i = 0; i < 6; i++)
        {
            Projectile newProjectile = Instantiate(projectile, (transform.position + (5 * direction)), Quaternion.identity).GetComponent<Projectile>();
            newProjectile.setDirection(direction);
            newProjectile.setColour(weaponColor);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator ShootShotgun()
    {
        shotgunCanShoot = false;
        yield return new WaitForSeconds(timeBetweenShotgunShots);
        shotgunCanShoot = true;
    }

    IEnumerator BlinkingWeapon()
    {
        if (isShotgun)
        {
            GameObject temp = canvas.transform.Find("Weapon1_P2").gameObject;
            for (int i = 0; i < 10; i++)
            {
                temp.SetActive(!temp.activeSelf);
                if (!isShotgun)
                {
                    break;
                }
                yield return new WaitForSeconds(.25f);
            }
        }
        else
        {

            GameObject temp = canvas.transform.Find("Weapon2_P2").gameObject;
            for (int i = 0; i < 10; i++)
            {
                temp.SetActive(!temp.activeSelf);
                if (isShotgun)
                {
                    break;
                }
                yield return new WaitForSeconds(.25f);
            }
        }

    }

    public void swapColour()
    {
        if (weaponColor == Colour.Red)
            weaponColor = Colour.Blue;
        else
            weaponColor = Colour.Red;
    }
    public void swapLiquor()
    {

        hasLiquor = !hasLiquor;
        if (hasLiquor)
            anim.SetBool("caughtWeapon", false);
            anim.SetBool("isCatching", true);
            anim.SetBool("hasLiquor", hasLiquor);

        health = 100;
    }

    public void SwapWeaponUI()
    {
        anim.SetBool("isCatching", true);
        anim.SetBool("caughtWeapon", true);

        GameObject temp = canvas.transform.Find("Weapon1_P2").gameObject;
        temp.SetActive(!temp.activeSelf);
        temp = canvas.transform.Find("Weapon2_P2").gameObject;
        temp.SetActive(!temp.activeSelf);
    }

    void CheckDeath()
    {
        if (health < 0)
        {
            anim.SetBool("isDead", true);
            StartCoroutine(DeathSceneLoad());

        }
    }

    IEnumerator DeathSceneLoad()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);

    }
}
