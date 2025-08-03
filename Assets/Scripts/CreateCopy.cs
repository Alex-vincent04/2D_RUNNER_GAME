using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCopy : MonoBehaviour
{
    public GameObject[] obstacle=new GameObject[5];
    public GameObject collectable;
    public GameObject PowerUp;
    public GameObject Point2x;
    public GameObject Coin2x;
    public GameObject Speed2x;
    public float Xinstantiate;
    public int obs;
    public GameObject find;
    public float Zinstantiate;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        /*Rigidbody find=GameObject.Find("Plane").GetComponent<Rigidbody>();*/
        InvokeRepeating("ObstacleCopy", 2f, 3f);
        InvokeRepeating("coin", 2f, 2f);
        InvokeRepeating("coin_0", 1f, 4f);
        InvokeRepeating("Powerup", 10f, 20f);
        InvokeRepeating("PointDouble", 5f, 20f);
        InvokeRepeating("CoinDouble", 5f, 20f);
        InvokeRepeating("SpeedDouble", 10f, 20f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ObstacleCopy()
    {
        obs = Random.Range(0, 10);
        Xinstantiate = Random.Range(-3.35f,3.35f);
        float Yposition = 0.5f;
        if (obs >= 3 && obs <9)
        {
            Yposition = 0.0f;
            /*Instantiate(obstacle[obs], new Vector3(Xinstantiate, Yposition, -5.209f), Quaternion.Euler(0f,180f,0));*/
        }
        else
        {
            Yposition = 0.5f;
        }
        Instantiate(obstacle[obs],new Vector3(Xinstantiate, Yposition, -5.209f),Quaternion.identity);
    }

    public void coin()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, -5f);
        Instantiate(collectable, new Vector3(Xinstantiate, 2.2f, Zinstantiate),Quaternion.identity);

        if (player.Coin2x)
        {
            Debug.Log("Coin");
            Instantiate(collectable, new Vector3(Xinstantiate, 2.2f, Zinstantiate), Quaternion.identity);
        }
    }
    public void coin_0()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, 5f);
        Instantiate(collectable, new Vector3(Xinstantiate, 2.2f, Zinstantiate), Quaternion.identity);
    }

    public void Powerup()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, 5f);
        Instantiate(PowerUp, new Vector3(Xinstantiate, 1.0f, Zinstantiate), Quaternion.identity);
    }
    public void PointDouble()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, 5f);
        Instantiate(Point2x, new Vector3(Xinstantiate, 1.0f, Zinstantiate), Quaternion.identity);
    }

    public void CoinDouble()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, 5f);
        Instantiate(Coin2x, new Vector3(Xinstantiate, 1.0f, Zinstantiate), Quaternion.identity);
    }

    public void SpeedDouble()
    {
        Xinstantiate = Random.Range(-3.35f, 3.35f);
        Zinstantiate = Random.Range(-15f, 5f);
        Instantiate(Speed2x, new Vector3(Xinstantiate, 1.0f, Zinstantiate), Quaternion.identity);
    }

}
