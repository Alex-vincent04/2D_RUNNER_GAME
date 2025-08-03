using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public int CoinSpeed;
    private Player player;

    void Start()
    {
        // Find the player once at the beginning
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();

            if (player == null)
            {
                Debug.LogWarning("Player script not found on object with tag 'Player'");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found");
        }
    }

    void Update()
    {
        // If player is not found or destroyed, try to find it again once
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
            }

            if (player == null)
            {
                // Cannot move coin if no player is found
                return;
            }
        }

        // Move the coin based on player's speed state
        if (player.Speed2x)
        {
            transform.Translate(Vector3.back * Time.deltaTime * CoinSpeed * 2);
        }
        else
        {
            transform.Translate(Vector3.back * Time.deltaTime * CoinSpeed);
        }

        // Destroy if out of view
        if (transform.position.z < -30f)
        {
            Destroy(gameObject);
        }
    }
}
