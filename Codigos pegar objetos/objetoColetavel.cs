using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetoColetavel : MonoBehaviour//tem que colocar no objeto a ser pego
{
    public int tipoDoColetavel = 0;
    // 0 - erro
    // 1 - moeda

    public int indiceDoObjeto = 0;

    private bool primeiraVez = true;

    private GameObject gameObjectScripts;
    private salvarProgresso salvarProgressoScript;

    // Start is called before the first frame update
    void Start()
    {


        switch (tipoDoColetavel)//verifica se o objeto colocado eh valido, se for 0 ou maior que a quantidade total de objetos ele se destroi e avisa no log
        {
            case 0:
                Debug.Log("Objeto 0 selecionado");
                Destroy(gameObject);
                break;

            case var expression when tipoDoColetavel > 1://tem que mudar para a quantidade de tipos de coletaveis
                Debug.Log("Objeto nao existente selecionado");
                Destroy(gameObject);
                break;
        }

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Scripts");
        gameObjectScripts = objs[0];
        salvarProgressoScript = gameObjectScripts.GetComponent<salvarProgresso>();


    }

    // Update is called once per frame
    void Update()
    {
        if (primeiraVez == true)//esta aqui no update, pois ele tentava acessar antes de que o salvarProgresso pudesse carregar as informacoes e assim dava erro, se tiver um jeito melhor seria bom, mas nao encontrei
        {
            primeiraVez = false;
            int valor = -1;

            valor = salvarProgressoScript.getValorObjeto(tipoDoColetavel,indiceDoObjeto);//carrega o valor do objeto que esta salvo e se ele ja estiver pego, esse gameObject sera destruido

            if (valor < 0)
                Debug.Log("Problema em carregar valor salvo");

            if (valor <= 0)
                Destroy(gameObject);
        }
    }

    public void objetoPego()//quando o objeto for pego, ele ira atualizar o valor para 0, ira salvar ele e destruir o objeto
    {
        salvarProgressoScript.atualizarObjeto(tipoDoColetavel, indiceDoObjeto, 0);
        Destroy(gameObject);
    }
}
