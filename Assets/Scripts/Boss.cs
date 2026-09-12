using UnityEngine;

public class Boss : MonoBehaviour
{
    private int hitCount = 0;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.white; // initial colour
    }

    public void TakeHit()
    {
        hitCount++;

        if (hitCount == 1)
        {
            rend.material.color = Color.yellow;
        }
        else if (hitCount == 2)
        {
            rend.material.color = new Color(1f, 0.5f, 0f); // orange
        }
        else if (hitCount == 3)
        {
            rend.material.color = Color.red;
        }
        else if (hitCount >= 4)
        {
            Destroy(gameObject); // boss defeated
        }
    }
}
