using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    public int Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * Speed);

        if (transform.position.z < -30f)
        {
            Destroy(gameObject);
        }
    }
}
