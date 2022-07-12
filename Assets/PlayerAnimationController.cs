using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isDie");
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDieEvent()
    {
        Debug.Log("End of Die Animation");
    }
}
