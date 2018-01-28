using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 direction;
    public float speed;

    public Material redMaterial;
    public Material blueMaterial;

    private Vector3 aimDirection;
    private Quaternion rotation1;
    private ParticleSystem bulletPart;

    // Use this for initialization
    void Start () {
        if (tag == "Red")
            GetComponent<Renderer>().material = redMaterial;
        else if (tag == "Blue")
            GetComponent<Renderer>().material = blueMaterial;

    }

    // Update is called once per frame
    void Update () {
        Vector3 newPosition = transform.position;
        newPosition += direction * speed * Time.deltaTime;
        transform.position = newPosition;

        
        
        
    }

    private void LateUpdate()
    {
        //bulletPart = gameObject.GetComponentInChildren<Animtor>.GetChild(0).Find("ParticleSystem");
        ////AimDirection = direction * 90;
        //rotation1 = Quaternion.LookRotation(direction);
        ////rotation.y = 45;
        ////rotation.x += 45;
        //bulletPart.rotation = rotation1;
        ////Debug.Log(rotation);
    }

    public void setDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void setColour(Colour newCol)
    {
        tag = newCol.ToString();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "WorldBounds")
        {
            Destroy(gameObject);
        }
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
