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
    public bool hasSwapped;
    

    private Vector3 playerPosition;
    private bool isShooting = false;
    private enum State { stand, shoot, walk, pending };
    private State currState;
    private int shotsFired = 0;
    public Colour thisColour;
    private GameController gc;
    private GameObject targetPlayer;
    private GameObject originalPlayer;
    public float enemyProjectileSpeed;
    private Animator anim;
    private Vector3 directionFace;
    private Quaternion rotation;

    private bool alreadyCollided;

    private bool enemyFiring;

    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
        currState = State.walk;
        projectileHolder = GameObject.Find("Projectiles");
        //playerPosition = targetPlayer.transform.position;
        target.y = transform.position.y;

        if (thisColour == Colour.Red)
            GetComponent<Renderer>().material = redMaterial;
        else if (thisColour == Colour.Blue)
            GetComponent<Renderer>().material = blueMaterial;

        enemyFiring = false;
        hasSwapped = false;
    }

    // Update is called once per frame
    void Update () {
        alreadyCollided = false;
        playerPosition = targetPlayer.transform.position;

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isFiring", true);
            enemyFiring = true;

            fire(Vector3.right);
        }

        if (currState == State.walk)
        {
            anim.SetBool("isFiring", false);
            enemyFiring = false;
            anim.SetBool("isMoving", true);
            StartCoroutine("handleWalking");
        }
        else if (currState == State.shoot)
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isFiring", true);
            enemyFiring = true;

            StartCoroutine("handleShooting");
        }
        else if (currState == State.stand)
        {

            StartCoroutine("handleStanding");
        }
        else if (currState == State.pending)
        {
        
        }
        //rotation = Quaternion.LookRotation(-target);
        //if()

        
    }

    private void LateUpdate()
    {
        if (enemyFiring)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((playerPosition - transform.position).normalized), 1.0f);
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target - transform.position).normalized), .75f);
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
        Vector3 gunPosition = transform.position;
        gunPosition.y = +GetComponent<CapsuleCollider>().height * 0.75f;

        anim.SetBool("isFiring", true);
        enemyFiring = true;

        EnemyProjectile newProjectile = Instantiate(projectile, gunPosition, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.transform.SetParent(projectileHolder.transform);
        newProjectile.setDirection(fireDirection);
        newProjectile.setSpeed(enemyProjectileSpeed);
    }
    void fireAtPlayer()
    {
        anim.SetBool("isFiring", true);
        enemyFiring = true;

        Vector3 gunPosition = transform.position;
        gunPosition.y = +GetComponent<CapsuleCollider>().height * 1.5f;

        EnemyProjectile newProjectile = Instantiate(projectile, gunPosition, Quaternion.identity).GetComponent<EnemyProjectile>();
        newProjectile.transform.SetParent(projectileHolder.transform);
        Vector3 direction = playerPosition - transform.position;
        direction.y = 0;
        newProjectile.setDirection(direction);
        newProjectile.setSpeed(enemyProjectileSpeed);
    }

    //Hit by projectile
    private void OnTriggerEnter(Collider collision)
    {
        if (alreadyCollided)
            return;

        if(collision.gameObject.tag == thisColour.ToString())
        {
            hitPoints--;
            Destroy(collision.gameObject);
        }
        if(hitPoints<1)
        {
            alreadyCollided = true;
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
        int prob = Random.Range(0, 100);
        if (prob < 50)
        {
            this.GetComponentInChildren<SkinnedMeshRenderer>().material = redMaterial;
            thisColour = Colour.Red;
        }
        else
        {
            this.GetComponentInChildren<SkinnedMeshRenderer>().material = blueMaterial;
            thisColour = Colour.Blue;
        }

    }
    public void ShotGunHit()
    {
        if (alreadyCollided)
            return;
        hitPoints--;
        if (hitPoints < 1)
        {
            alreadyCollided = true;
            gc.registerEnemyDead(originalPlayer);
            Destroy(this.gameObject);
        }
    }

    public void changeAggro()
    {
        if (targetPlayer == GameObject.Find("Player1"))
        {
            targetPlayer = GameObject.Find("Player2");
        }
        else if (targetPlayer == GameObject.Find("Player2"))
        {
            targetPlayer = GameObject.Find("Player1");
        }
        hasSwapped = true;
    }

    public bool checkTarget(GameObject playerToCheck)
    {
        if (targetPlayer == playerToCheck)
        {
            return true;

        }
        else
            return false;
    }

}
