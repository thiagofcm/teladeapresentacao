using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morcegoMove : MonoBehaviour
{

    public float velocidadeh;
    public float velocidadev;
    public float min;
    public float max;
    public float espera;
    private GameObject player;
    private bool pontuou;

    void Start()
    {
        StartCoroutine(Move(min));
        player = GameObject.Find("Player");
        pontuou = false; 
    }
    IEnumerator Move(float destino)
    {
        while (Mathf.Abs(destino - transform.position.y) > 0.2f)
        {
            Vector3 direcao = (destino == max) ? Vector3.up : Vector3.down;
            Vector3 velocidadeVetorial = direcao * velocidadev;
            transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));
    }

    void Update()
    {
        Vector3 velocidadeVetorialv = Vector3.left * velocidadeh;
        transform.position = transform.position + velocidadeVetorialv * Time.deltaTime;
        if (GameController.instancia.estado == Estado.Jogando)
        {
            if (!pontuou && transform.position.x < player.gameObject.transform.position.x)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    GameController.instancia.acrescentarPontos(1);
                    pontuou = true;
                }
            }
        }
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
}