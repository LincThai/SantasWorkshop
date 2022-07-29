using UnityEngine;

public class Target : MonoBehaviour
{
    // set variables
    public float health = 50f;

    // function for taking damage
    public void TakeDamage(float amount)
    {
        // reduce health by amount
        // amount = damage from gun
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    // death function
    void Die()
    {
        // destroys the game object this script is attached to
        Destroy(gameObject);
    }
}
