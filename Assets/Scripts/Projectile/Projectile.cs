using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 direction;
    public float speed;

    // Use this for initialization
    void Start () {
        if (tag == "Red")
            colourRed();
        else if (tag == "Blue")
            colourBlue();
    }

    // Update is called once per frame
    void Update () {
        Vector3 newPosition = transform.position;
        newPosition += direction * speed * Time.deltaTime;
        transform.position = newPosition;
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
    private void colourRed()
    {
        transform.GetChild(0).Find("ParticleSystemRed").gameObject.SetActive(true);
        transform.GetChild(0).Find("ParticleSystemBlue").gameObject.SetActive(false);
    }
    private void colourBlue()
    {
        transform.GetChild(0).Find("ParticleSystemRed").gameObject.SetActive(false);
        transform.GetChild(0).Find("ParticleSystemBlue").gameObject.SetActive(true);
    }
}
