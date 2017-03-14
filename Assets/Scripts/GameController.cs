using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float espera;
    public float tempoDestruicao;
    public GameObject obstaculo;

    public static GameController instancia = null;

    void Awake()

    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (instancia != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(GerarObstaculos());
    }
    IEnumerator GerarObstaculos()
    {
        while (true)
        {
            Vector3 pos = new Vector3(7f, Random.Range(-1f, 3.0f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }
    void Update()
    {

    }
}
