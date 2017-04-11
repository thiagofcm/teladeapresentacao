using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Estado estado { get; private set; }
    public GameObject menu;
    public GameObject canvas;

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
        estado = Estado.AguardoComecar;
    }
    IEnumerator GerarObstaculos()
    {
        while (GameController.instancia.estado == Estado.Jogando)
        {
            Vector3 pos = new Vector3(12f, Random.Range(0.5f, 8f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menu.SetActive(false);
        canvas.SetActive(false);
        StartCoroutine(GerarObstaculos());
    }   
    public void PlayerMorreu()
    {
        estado = Estado.GameOver;
    }
}
