using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPegarObjetos : MonoBehaviour//tem que colocar no player
{
    // Start is called before the first frame update
    void Start()
    {
        //comando para apagar tudo que esta salvo
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)//verifica se o player encostou em um objeto pegavel e se for ele aciona a funcao de pegar o objeto
    {
        if (other.gameObject.tag == "Coletavel")
        {
            objetoColetavel script = other.GetComponent<objetoColetavel>();
            script.objetoPego();
        }

    }
}
