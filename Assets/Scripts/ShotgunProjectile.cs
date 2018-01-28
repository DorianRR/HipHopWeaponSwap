using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour {

    private Vector3 direction;
    public float speed;

    public Material redMaterial;
    public Material blueMaterial;

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
        if(collider.gameObject.tag == "Bad")
        {
            collider.gameObject.GetComponent<Enemy>().ShotGunHit();
            Destroy(this.gameObject);
        }
    }
}
