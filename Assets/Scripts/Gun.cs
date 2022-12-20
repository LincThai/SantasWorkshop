using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    // Set variables
    
    [Header("Stat Values")]
    // Set damage Variable
    public float damage = 10f;
    // Set gun range
    public float range = 100f;
    // Set rate of fire
    public float fireRate = 15f;
    // Set impact force
    public float impactForce = 30f;

    [Header("Ammunition Values")]
    // Set Max Ammo
    public int maxAmmo = 10;
    // Set Current Ammo
    private int currentAmmo;
    // Set Reload Time
    public float reloadTime = 1f;
    // Set Reload bool
    private bool isReloading = false;

    
    [Header("Refecences")]
    // Reference to camera
    public Camera fpsCam;
    // Reference to particle system
    public ParticleSystem muzzleFlash;
    // Reference to impact effect game object/particle system
    public GameObject impactEffect;

    // Private float for the next time to fire
    private float nextTimeToFire = 0f;

    // reference to animator
    public Animator animator;

    void Start()
    {
        // set currentAmmo to maxAmmo
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        // resets both bools when the gameobjects is enabled
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (isReloading)
        {
            // sends back to the start
            return;
        }

        // check if ammo is less than 0
        if (currentAmmo <= 0)
        {
            // starts thew reload function
            StartCoroutine(Reload());
            // sends back to the start
            return;
        }

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

    // function to reload the gun
    IEnumerator Reload()
    {
        // set is reloading to false
        isReloading = true;
        Debug.Log("Reloading...");
        // set reloading bool in animator to true
        animator.SetBool("Reloading", true);
        // make you wait for reloadTime - .25 seconds
        yield return new WaitForSeconds(reloadTime - .25f);
        // set reloading bool in animator to false
        animator.SetBool("Reloading", false);
        // wait .25 seconds for Animation to be complete
        yield return new WaitForSeconds(.25f);
        // reload
        currentAmmo = maxAmmo;
        // set is reloading to false
        isReloading = false;
    }

    // function to shoot from the gun
    void Shoot()
    {
        // play muzzle flash
        muzzleFlash.Play();

        // reduce ammo
        currentAmmo--;
        
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
