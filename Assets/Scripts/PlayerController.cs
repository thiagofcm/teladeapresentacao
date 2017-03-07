using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float ForcaDoPulo = 10f;

    private Animator anim;
    private Rigidbody rb;
    private bool pulando = false;

	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            anim.Play("pulando");
            rb.useGravity = true;
            pulando = true;
        }
	}

    private void FixedUpdate()
    {
        if (pulando)
        {
            pulando = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * ForcaDoPulo, ForceMode.Impulse); 
        }
    }

}
