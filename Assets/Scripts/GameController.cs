using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Estado estado { get; private set; }
    public GameObject menu;
    public GameObject painelMenu;
    private int pontos;
    public Text txtPontos;
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
        painelMenu.SetActive(false);
        painelMenu.SetActive(false);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }   
    public void PlayerMorreu()
    {
        estado = Estado.GameOver;
    }

    private void atualizarPontos(int x) {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void acrescentarPontos (int x) {
        atualizarPontos(pontos + x);
    }
}
