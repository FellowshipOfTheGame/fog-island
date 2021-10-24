using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class salvarProgresso : MonoBehaviour//tem que colocar em um gameObject que sempre existira
{
    //informacoes a serem salvas no playerPrefs
    private int[] moedas = new int[0];

    public int qtdMoedas = 5;


    //no tipo as coisas sao:
    //1 - moeda

    // Start is called before the first frame update
    void Start()
    {
        Array.Resize(ref moedas, qtdMoedas);//loadar as coisas ja salvas, tem que mudar quando adicionar mais coisas
        for (int i = 0; i < qtdMoedas; i++)
        {
            moedas[i] = PlayerPrefs.GetInt("moedas" + (i-1).ToString(), 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void salvarDados()//salvar as coisas, tem que mudar se adicionar mais coisas
    {
        for (int i = 0; i < qtdMoedas; i++)
        {
            PlayerPrefs.SetInt("moedas" + (i - 1).ToString(), moedas[i]);
        }
        PlayerPrefs.Save();
    }

    public void atualizarObjeto(int tipo, int index, int valor)//atualiza o valor do objeto no index
    {
        switch (tipo)//tem que mudar se adicionar mais coisas
        {
            case 1:
                moedas[index] = valor;
                break;

            default:
                break;
        }
        salvarDados();

    }

    public int getValorObjeto(int tipo, int index)//pega o valor do tipo no indice e o retorna
    {
        switch (tipo)//tem que mudar se adicionar mais tipos
        {
            case 1:
                return moedas[index];

            default:
                return -1;
        }
    }
}
