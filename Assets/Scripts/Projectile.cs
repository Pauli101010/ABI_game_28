using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeHit();
                Debug.Log("Boss hit by projectile.");
            }
        }

        // Destroy projectile no matter what it hits
        Destroy(gameObject);
    }
}
