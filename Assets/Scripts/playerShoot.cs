using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{

    public GameObject projectile;
    private GameObject projectileHolder;
    public GameObject playerOne;
    //public GameObject playerTwo;
    // Use this for initialization
    public float walkSpeed;
    public float wanderRange;
    public float shootInterval;
    public int numberShots;
    private Vector3 playerPosition;

    private bool isShooting = false;
    private enum State { stand, shoot, walk, pending };
    private State currState;
    private int shotsFired = 0;

    void Start()
    {
        currState = State.walk;
        projectileHolder = GameObject.Find("Projectiles");
        playerPosition = playerOne.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerOne.transform.position;
        if (Input.GetMouseButtonUp(0))
        {
            fire(Vector3.right);
        }

        if (currState == State.walk)
        {
            StartCoroutine("handleWalking");
        }
        else if (currState == State.shoot)
        {
            StartCoroutine("handleShooting");
        }
        else if (currState == State.stand)
        {
            StartCoroutine("handleStanding");
        }
        else if (currState == State.pending)
        {

        }

    }


    IEnumerator handleShooting()
    {
        currState = State.pending;


        if (shotsFired < numberShots)
        {
            //fire(Vector3.right);
            fireAtPlayer();
            shotsFired++;
            currState = State.stand;
        }
        else
        {
            currState = State.walk;
        }

        yield return null;
    }

    IEnumerator handleStanding()
    {
        currState = State.pending;

        yield return new WaitForSeconds(shootInterval);

        currState = State.shoot;
    }

    IEnumerator handleWalking()
    {
        currState = State.pending;

        float bufferDistance = walkSpeed * .5f;
        bool foundValidPos = false;
        Vector3 target = new Vector3();
        while (!foundValidPos)
        {
            Vector3 offset = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
            offset *= wanderRange;
            target = offset + transform.position;

            if (!Physics.Raycast(transform.position, offset, wanderRange))
                foundValidPos = true;
            else
            {
                offset.x *= -1;
                offset.z *= -1;
                target = offset + transform.position;
                if (!Physics.Raycast(transform.position, offset, wanderRange))
                    foundValidPos = true;
            }
        }
        while (Vector3.Distance(target, transform.position) > bufferDistance)
        {
            Vector3 newPos = transform.position;
            newPos += (target - transform.position).normalized * walkSpeed * Time.deltaTime;
            transform.position = newPos;

            yield return null;
        }

        currState = State.stand;
        shotsFired = 0;
    }

    void fire(Vector3 fireDirection)
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.transform.SetParent(projectileHolder.transform);
        newProjectile.setDirection(fireDirection);
    }
    void fireAtPlayer()
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.transform.SetParent(projectileHolder.transform);
        Vector3 direction = playerPosition - transform.position;
        direction.y = transform.position.y;
        newProjectile.setDirection(direction);
    }

    //Hit by projectile
    private void OnCollisionEnter(Collision collision)
    {
       
    }


}
