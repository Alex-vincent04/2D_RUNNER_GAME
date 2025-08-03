using Unity.VisualScripting;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public int RoadSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*Time.deltaTime*RoadSpeed);

        if (transform.position.z < -10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 24.1f);
        }
    }

    //void Speed2x()
    //{
    //    transform.Translate(Vector3.back * Time.deltaTime * RoadSpeed);

    //    if (transform.position.z < -20f)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, 24.1f);
    //    }
    //}
}
