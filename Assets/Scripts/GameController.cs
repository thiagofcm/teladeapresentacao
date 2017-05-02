using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public GameObject menuCamera;
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public GameObject pontosPanel;
    public Text txtMaiorPontuacao;
    private List<GameObject> obstaculos;


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
        obstaculos = new List<GameObject>();
        PlayerPrefs.SetInt("HighScore", 0);
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
    }
    IEnumerator GerarObstaculos()
    {
        while (GameController.instancia.estado == Estado.Jogando)
        {
            Vector3 pos = new Vector3(12f, Random.Range(0.5f, 8f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            obstaculos.Add(obj);
            StartCoroutine(DestruirObstaculo(obj));
            yield return new WaitForSeconds(espera);
        }
    }

    IEnumerator DestruirObstaculo(GameObject obj)
    {
        yield return new WaitForSeconds(tempoDestruicao);
        if (obstaculos.Remove(obj))
        {
            Destroy(obj);
        }
    }

    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        pontosPanel.SetActive(true);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }   
    public void PlayerMorreu()
    {
        estado = Estado.GameOver;
        if (pontos > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", pontos);
            txtMaiorPontuacao.text = "" + pontos;
        }
        gameOverPanel.SetActive(true);
    }

    private void atualizarPontos(int x) {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void acrescentarPontos (int x) {
        atualizarPontos(pontos + x);
    }


    public void PlayerVoltou()
    {

        while (obstaculos.Count > 0)
        {
            GameObject obj = obstaculos[0];
            if (obstaculos.Remove(obj))
            {
                Destroy(obj);
            }
        }
        estado = Estado.AguardoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("Zeca").GetComponent<PlayerController>().recomecar();
    }
}

