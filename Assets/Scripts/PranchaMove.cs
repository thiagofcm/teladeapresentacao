using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PranchaMove : MonoBehaviour {

    public float velocidade;
    public float limite;
    public float retorno;
	void Start () {
		
	}
	
	void Update () {
        Vector3 velocidadeVetorial = Vector3.left * velocidade;
        transform.localPosition = transform.localPosition + velocidadeVetorial * Time.deltaTime;

        if(transform.localPosition.x <= limite)
        {
            transform.localPosition = new Vector3(retorno, transform.localPosition.y, transform.localPosition.z);
        }
	}
}
