using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerVida : MonoBehaviour//classe colocada no player
{
    public int vidaMax = 10;//vida maxima do player
    public float tempoHeal = 5f;//tempo para a vida comecar regenerar(segundos)
    public int curaPorTick = 1;//quanto cura por tick de heal
    public float tempoPorTickHeal = 1;//tempo para cada tick acontecer(segundos)
    public Vector3 posicaoRespawn = new Vector3(0,0,0);//posicao em que o player ira teleportar quando morrer
    public Image imagemFade;//imagem que aparecera para o fade
    public int duracaoFade = 2;//duracao do fade em segundos(fadeIN e o fadeOUT)

    private int vidaAtual;//vida atual do player
    private bool danoTomado = false;//verifica se o player tomou dano recentemente
    private float tempoPassado = 0;//tempo que passou desde o ultimo dano
    private float tempoPassadoHeal = 0;//tempo que passou desde o ultimo tick de heal

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        //para testes
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            danoAplicado(1);
        }
        */

        if (danoTomado == true && vidaAtual < vidaMax)//verifica se tomou dano e se tomou comeca a contar o tempo para poder comecar a curar
            tempoPassado += Time.deltaTime;

        if (tempoPassado >= tempoHeal)//se o tempo minimo foi atingido, comeca a contar os ticks
        {
            tempoPassadoHeal += Time.deltaTime;
            if (tempoPassadoHeal >= tempoPorTickHeal)//se ja passou tempo suficiente de um tick, ele cura e reseta o contador de tick
            {
                tempoPassadoHeal = 0;
                curarVida(curaPorTick);
                if (vidaAtual >= vidaMax)//se estiver com a vida cheia, muda o dano tomado para false
                {
                    tempoPassado = 0;
                    danoTomado = false;
                }
            }
        }
    }

    public void danoAplicado(int dano)//aplica dano ao jogador
    {
        danoTomado = true;
        tempoPassado = 0;
        vidaAtual -= dano;

        if (vidaAtual <= 0)
            playerMorreu();
    }

    private void playerMorreu()//comeca a animacao
    {
        StartCoroutine(FadeImagem(false));
        //todo fazer o player nao pode se mexer
    }

    private void teleportarPlayerRespawn()//teleporta o player e termina a execucao do respawn
    {
        gameObject.transform.position = posicaoRespawn;
        StartCoroutine(FadeImagem(true));//faz o fadeOut
        //reinicia a vida e os contadores
        vidaAtual = vidaMax;
        danoTomado = false;
        tempoPassado = 0;
        tempoPassadoHeal = 0;
    }

    public void curarVida(int cura)//cura o player na quantidade da cura
    {
        vidaAtual += cura;
        if (vidaAtual > vidaMax)
            vidaAtual = vidaMax;
    }

    IEnumerator FadeImagem(bool fadeOut)
    {
        //fade out
        if (fadeOut == true)
        {
            for (float i = duracaoFade; i >= 0; i -= Time.deltaTime)
            {
                imagemFade.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        //fade in
        else
        {
            for (float i = 0; i <= duracaoFade; i += Time.deltaTime)
            {
                imagemFade.color = new Color(1, 1, 1, i);
                yield return null;
            }
            teleportarPlayerRespawn();//quando a tela esta totalmente com a imagem, ele teleporta o player
        }
    }
}
