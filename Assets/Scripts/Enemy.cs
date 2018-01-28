using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colour { Red, Blue };

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    private GameObject projectileHolder;
    //public GameObject playerTwo;
    // Use this for initialization
    public float walkSpeed;
    public float wanderRange;
    public float shootInterval;
    public int numberShots;
    public Material redMaterial;
    public Material blueMaterial;
    public int hitPoints;
    public Vector3 target;
    

    private Vector3 playerPosition;
    private bool isShooting = false;
    private enum State { stand, shoot, walk, pending };
    private State currState;
    private int shotsFired = 0;
    public Colour thisColour;
    private GameController gc;
    private GameObject targetPlayer;
    private GameObject originalPlayer;


    void Start () {
        currState = State.walk;
        projectileHolder = GameObject.Find("Projectiles");
        playerPosition = targetPlayer.transform.position;
        target.y = transform.position.y;

        if (thisColour == Colour.Red)
            GetComponent<Renderer>().material = redMaterial;
        else if (thisColour == Colour.Blue)
            GetComponent<Renderer>().material = blueMaterial;
    }

    // Update is called once per frame
    void Update () {
        playerPosition = targetPlayer.transform.position;

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


        if ( shotsFired < numberShots)
        {
            //fire(Vector3.right);
            fireAtPlayer();
            shotsFired++;
            currState = State.stand;
        }
        else
        {
            moveRandomly();
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

        float bufferDistance = walkSpeed * .1f;


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
        direction.y = 0;
        newProjectile.setDirection(direction);
    }

    //Hit by projectile
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == thisColour.ToString())
        {
            hitPoints--;
            Destroy(collision.gameObject);
        }
        if(hitPoints<1)
        {
            gc.registerEnemyDead(originalPlayer);
            Destroy(this.gameObject);
        }
    }
    private void moveRandomly()
    {
        bool foundValidPos = false;
        while (!foundValidPos)
        {
            Vector3 offset = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
            offset *= wanderRange;
            target = offset + transform.position;

            if (!Physics.Raycast(transform.position, offset, wanderRange + GetComponent<Collider>().bounds.size.x))
                foundValidPos = true;
            else
            {
                offset.x *= -1;
                offset.z *= -1;
                target = offset + transform.position;
                if (!Physics.Raycast(transform.position, offset, wanderRange + GetComponent<Collider>().bounds.size.x ))
                    foundValidPos = true;
            }
        }

    }

    public void setInitialMovement(Vector3 initPosition)
    {
        target = initPosition;
    }

    public void setEnemyInfo(GameController gc, GameObject player)
    {
        this.gc = gc;
        this.targetPlayer = player;
        this.originalPlayer = player;
    }
    public void ShotGunHit()
    {
        hitPoints--;
        if (hitPoints < 1)
        {
            gc.registerEnemyDead(originalPlayer);
            Destroy(this.gameObject);
        }
    }
}
