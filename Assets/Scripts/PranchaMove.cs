using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PranchaMove : MonoBehaviour {

    public float velocidade;

	void Start () {
		
	}
	
	void Update () {
        Vector3 velocidadeVetorial = Vector3.left * velocidade;
        transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
	}
}
