using UnityEngine;

public class Gun : MonoBehaviour
{
    // Set variables
    
    [Header("Numeric Values")]
    // Set damage Variable
    public float damage = 10f;
    // set gun range
    public float range = 100f;
    // set rate of fire
    public float fireRate = 15f;
    // set impact force
    public float impactForce = 30f;

    [Header("Refecences")]
    // reference to camera
    public Camera fpsCam;
    // reference to particle system
    public ParticleSystem muzzleFlash;
    // reference to impact effect game object/particle system
    public GameObject impactEffect;

    // private float for the next time to fire
    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
       // checks if the fire button is pressed and
       // that the current time is greater than the nextTimeToFire variable
       if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            // sets the intervals between shots
            nextTimeToFire = Time.time + 1f / fireRate;

            // calls the Shoot function
            Shoot();
        }
    }

    // function to shoot from the gun
    void Shoot()
    {
        // play muzzle flash
        muzzleFlash.Play();
        
        // variable that will be assigned the gameObject that is hit
        RaycastHit hit;

        // checks if the raycast sent forward from the main camera hit something within the range
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // get reference to target script from the gameobject that is hit with the raycast and
            // assign to target variable 
            Target target = hit.transform.GetComponent<Target>();

            // check if there is a target script on the gameObject
            if (target != null)
            {
                // if true call TakeDamage function on target and assign amount = damage
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // instantiate the impact effect at the hit point of any game object that is hit, facing the direction of the hit
            // assign it to the variable impactGO
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            // destroy gameObject
            Destroy(impactGO, 2f);
        }
    }
}
