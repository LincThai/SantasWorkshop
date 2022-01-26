using UnityEngine;

public class Gun : MonoBehaviour
{
    // Set variables
    // Set damage Variable
    public float damage = 10f;
    // set gun range
    public float range = 100f;

    // reference to camera
    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
       // checks if the fire button is pressed
       if (Input.GetButtonDown("Fire1"))
        {
            // calls the Shoot function
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
