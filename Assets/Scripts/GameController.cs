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
    public GameObject menuCamera;
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public GameObject pontosPanel;
    public Text txtMaiorPontuacao;


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
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        pontosPanel.SetActive(true);
        acrescentarPontos(0);
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
        estado = Estado.AguardoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("Zeca").GetComponent<PlayerController>().recomecar();
    }
}

