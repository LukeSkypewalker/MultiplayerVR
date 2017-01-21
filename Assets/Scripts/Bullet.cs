using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public int damage;
	// Use this for initialization
    void OnCollisionEnter(Collision collision)
    {
        GameObject hitedGameObject = collision.gameObject;
        Health health = hitedGameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);

    }
}
