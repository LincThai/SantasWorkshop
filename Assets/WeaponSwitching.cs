using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    // variables
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        // call function
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // integer for the index of previousSelectedWeapon
        int previousSelectedWeapon = selectedWeapon;

        // get scroll wheel positive input
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                // selectedWeapon = 0
                selectedWeapon = 0;
            }
            else
            {
                // adds 1 to selected weapon
                selectedWeapon++;
            }
        }
        // get scroll wheel negative input
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                // selectedWeapon = 0
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                // subtracts 1 from selected weapon
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        // checks if current selectedWeapon is not equal to previousSelectedWeapon
        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }
    void SelectWeapon()
    {
        // set i variable
        int i = 0;
        // checks for each weapon that is the shild of this gameObject
        foreach (Transform weapon in transform)
        {
            // checks if i is equal to the selectedWeapon
            if (i == selectedWeapon)
            {
                // if true set the weapon gameObject active is true
                weapon.gameObject.SetActive(true);
            }
            else
            {
                // if false set the weapon gameObject active is false
                weapon.gameObject.SetActive(false);
            }
         // Adds 1 to i
         i++;
        }
    }
}
