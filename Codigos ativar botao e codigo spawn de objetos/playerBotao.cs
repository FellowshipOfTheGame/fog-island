using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBotao : MonoBehaviour
{
    public string tagBotao;//tag que os botoes terao

    public string funcaoDeAtivarBotao;//tag que os botoes terao

    public KeyCode teclaInteracao = KeyCode.E;//tecla de interacao

    public float distanciaDeAtivamento = 4f;//distancia maxima que o player pode interagir com algo

    public float delayEntreClick = 0.01f;//tempo minimo entre clicks(em segundos)

    private float proximoClick;//tempo do proximo click

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(teclaInteracao) && Time.time > proximoClick)//procura o botao mais proximo e clica se for valido
        {
            proximoClick = Time.time + delayEntreClick;//calcula quando pode apertar o botao novamente

            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag(tagBotao);//pega todos os objetos com a tag Botao

            float[] objetosPerto = new float[objs.Length];//a distancia entre o player e todos os botoes

            for (int i = 0; i < objs.Length; i++)//calcula a distancia entre todos os botoes e o player e guarda no vetor
            {
                objetosPerto[i] = Vector3.Distance(objs[i].gameObject.transform.position, transform.position);
            }


            float menorDist = objetosPerto[0];
            int indiceMenor = 0;
            for (int i = 1; i < objetosPerto.Length; i++)//procura pelo indice do botao mais perto do player
            {
                if (objetosPerto[i] < menorDist)
                {
                    menorDist = objetosPerto[i];
                    indiceMenor = i;
                }
            }

            if (menorDist <= distanciaDeAtivamento)//se o botao esta perto o suficiente para ser clicado, ele sera clicado
            {
                objs[indiceMenor].SendMessage(funcaoDeAtivarBotao);
            }


        }
    }
}
