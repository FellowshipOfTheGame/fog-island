using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botaoSpawnObjeto : MonoBehaviour
{
    public enum tipoDoBotao { ativar, reset };//tipos de botoes diferentes para esse puzzle
    //ativar - botao normal que executa normalmente
    //reset - botao que apaga todos os objetos em um raio

    public tipoDoBotao tipo;//tipo desse botao
    public Vector3 localDoSpawn;//local em o objeto sera spawnado(sendo a distancia entre o botao)
    public GameObject prefabObjeto;//objeto a ser spawnado
    public float raioDeClear = 100f;//raio em que todos os objetos serao deletados
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botaoClicado()//chamada da classe do player de clicar o botao, essa funcao tem que estar em todos os puzzles com botoes(variando a sua funcao, mas o nome tem de ser o mesmo)
    {
        if (tipo == tipoDoBotao.ativar)
        {
            spawnObjeto();
        } else if (tipo == tipoDoBotao.reset)
        {
            clearObjetos();
        }
    }

    private void spawnObjeto()//funcao de spawnar o objeto
    {
        if (prefabObjeto != null)//verifica se o objeto foi selecionado
        {
            Vector3 result = gameObject.transform.position + localDoSpawn;//calcula a posicao do objeto
            Instantiate(prefabObjeto, result, Quaternion.identity);//spawna o objeto
        }
        else
        {
            Debug.Log("Objeto para Spawn nao selecionado!!");
            Destroy(gameObject);
        }
    }

    private void clearObjetos()//funcao de spawnar os objetos
    {
        if (prefabObjeto != null)//verifica se o objeto foi selecionado
        {
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag(prefabObjeto.tag);//pega todos os objetos com a mesma tag

            for (int i = 0; i < objs.Length; i++)//exclui todos esses objetos se eles estiverem perto o suficiente
            {
                if (Vector3.Distance(objs[i].gameObject.transform.position, transform.position) <= raioDeClear)
                {
                    Destroy(objs[i]);
                }
            }
        }
        else
        {
            Debug.Log("Objeto para Clear nao selecionado!!");
            Destroy(gameObject);
        }
    }
}
