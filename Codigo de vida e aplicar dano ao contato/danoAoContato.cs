using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danoAoContato : MonoBehaviour//classe colocada no objeto que ira aplicar o dano no player, o objeto precisa ter rigidbody(isKinematic eh ultil)
{
    public int dano = 1;//dano ao player
    public float tickDano = 0.5f;//quanto tempo para dar o dano no player, se ele continuar em contato
    public float delayTick = 0;//tempo de ficar em contato, ate dar o primeiro tick
    public int speedKnockbackHorizontal = 10;//knockbak na horizontal com o dano
    public int speedKnockbackVertical = 0;//knockbak na vertical com o dano

    private GameObject player;
    private playerVida playerVidaScript;
    private Rigidbody playerRigibody;
    private bool encostou = false;//o player esta encostado com o objeto
    private float tempoPassadoDelay = 0;//tempo para comecar a tomar dano
    private float tempoPassadoTick = 0;//tempo entre ticks de dano

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Player");

        player = objs[0];
        playerVidaScript = player.GetComponent<playerVida>();
        playerRigibody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (encostou == true)//se o player esta encostado, comeca a contar o tempo e aplica o dano no player quando o tempo tiver passado
        {
            tempoPassadoDelay += Time.deltaTime;
            if (tempoPassadoDelay >= delayTick)
            {
                tempoPassadoTick += Time.deltaTime;
                if (tempoPassadoTick >= tickDano)
                {
                    tempoPassadoTick = 0;
                    playerVidaScript.danoAplicado(dano);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)//verifica se o objeto chegou no chao para poder pular e andar denovo
    {
        if (other.gameObject.tag == "Player")
        {
            entrouEmContato();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            saiuDeContato();
        }
    }

    private void entrouEmContato()//player encostou com o objeto, comeca a contar os tempos e aplica o knockback no player
    {
        encostou = true;
        tempoPassadoDelay = 0;
        tempoPassadoTick = 0;
        if (delayTick <= 0)
        {
            playerVidaScript.danoAplicado(dano);
        }

        if (speedKnockbackHorizontal != 0 || speedKnockbackVertical != 0)
        {
            Vector3 velocidadeAtras = Vector3.zero;
            velocidadeAtras += player.transform.forward * -1;
            velocidadeAtras = velocidadeAtras * speedKnockbackHorizontal;
            velocidadeAtras.y = speedKnockbackVertical;
            playerRigibody.velocity = velocidadeAtras;
        }

    }

    private void saiuDeContato()//player desencostou com o objeto
    {
        encostou = false;
        tempoPassadoDelay = 0;
        tempoPassadoTick = 0;
    }


}
