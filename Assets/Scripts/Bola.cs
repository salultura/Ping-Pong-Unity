using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    [SerializeField]
    private float velocidadeInicial = 8;
    [SerializeField]
    private float velocidadeAtual;
    [SerializeField]
    private readonly float velocidadeMaxima = 30;
    [SerializeField]
    private float forcaRebatida = .5f;
    [SerializeField]
    private float tempoParaReiniciarPartida = 2;
    private Rigidbody rb;
    private Vector3 posicaoInicial;
    private Vector3 sentido;
    private Placar placar;
    [SerializeField]
    AudioClip audioPonto;
    [SerializeField]
    AudioClip audioHitRaquete;
    [SerializeField]
    AudioClip audioHitQuadra;
    private AudioSource playerDeAudio;

    // Use this for initialization
    void Awake()
    {
        posicaoInicial = transform.position;
        rb = GetComponent<Rigidbody>();
        playerDeAudio = GetComponent<AudioSource>();
        placar = FindObjectOfType<Placar>();
        velocidadeAtual = velocidadeInicial;
    }

    private void Start()
    {        
        StartCoroutine(ReposicionarBola());
    }

    private void Sacar()
    {
        if (Random.Range(0, 2) == 1)
        {
            sentido = Vector3.left;
        }
        else
        {
            sentido = Vector3.right;
        }
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y) > 7)
        {
            StartCoroutine(ReposicionarBola());
        }
    }

    private void FixedUpdate()
    {
        Vector3 direcao = sentido * velocidadeAtual * Time.deltaTime;
        rb.MovePosition(transform.position + (direcao));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "jogador")
        {
            sentido.y = (transform.position.y - other.transform.position.y) / 4;
            sentido.x *= -1;
            AumentaVelocidadeDaBola();
            playerDeAudio.PlayOneShot(audioHitRaquete);
        }

        if (other.tag == "quadra")
        {
            sentido.y *= -1;
            AumentaVelocidadeDaBola();
            playerDeAudio.PlayOneShot(audioHitQuadra);
        }

        if (other.tag == "gol-player-1")
        {
            sentido = Vector3.zero;
            placar.PontoPlayer2();
            StartCoroutine(ReposicionarBola());
            playerDeAudio.PlayOneShot(audioPonto);
        }

        if (other.tag == "gol-player-2")
        {
            sentido = Vector3.zero;
            placar.PontoPlayer1();
            StartCoroutine(ReposicionarBola());
            playerDeAudio.PlayOneShot(audioPonto);
        }
    }

    private void AumentaVelocidadeDaBola()
    {
        if (velocidadeAtual < velocidadeMaxima)
        {
            velocidadeAtual += forcaRebatida;
        }
    }

    private IEnumerator ReposicionarBola()
    {
        BolaAoCentro();
        yield return new WaitForSeconds(tempoParaReiniciarPartida);
        Sacar();
    }

    private void BolaAoCentro()
    {
        transform.position = posicaoInicial;
        velocidadeAtual = velocidadeInicial;
    }
}
