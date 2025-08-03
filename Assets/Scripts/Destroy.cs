using TMPro;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public int Speed;
    public TextMeshProUGUI Text;
    public float Score;

    private Player player;

    void Start()
    {
        // Try to find the player at the beginning
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();

            if (player == null)
            {
                Debug.LogWarning("No Player script found on object with tag 'Player'");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found");
        }
    }

    void Update()
    {
        // If player is still null, try finding again (in case player respawned)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
            }

            if (player == null)
            {
                // Still not found, stop update logic this frame
                return;
            }
        }

        // Move the object based on player's speed mode
        if (player.Speed2x)
        {
            transform.Translate(Vector3.back * Time.deltaTime * Speed * 2);
        }
        else
        {
            transform.Translate(Vector3.back * Time.deltaTime * Speed);
        }
    }

    public void ScoreAdd()
    {
        Score += 1;
        if (Text != null)
        {
            Text.text = "Score " + Score;
        }
        else
        {
            Debug.LogWarning("Score Text UI is not assigned.");
        }
    }
}
