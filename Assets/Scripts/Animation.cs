using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
    }
}
