  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{

    private float moveSpeed = 5.0f;
    //private Vector3 moveDirection = Vector3.zero;

    private Vector3 moveDirection;

    private Rigidbody2D rigid2D;

    //private void Awake()
    //{
    //    rigid2D = GetComponent<Rigidbody2D>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void Setup(Vector3 direction)
    {
        moveDirection = direction;
    }
    void Update()
    {   
        //Negatvie left , a : -1
        //Positive right, d : 1
        //None : 0
        //float x = Input.GetAxisRaw("Horizontal");
        ////Negatvie down , s : -1
        ////Positive up, w : 1
        ////None : 0
        //float y = Input.GetAxisRaw("Vertical");

        //이동방향 설정
        //moveDirection = new Vector3(x, y, 0);

        //transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
