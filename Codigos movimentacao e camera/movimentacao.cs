using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentacao : MonoBehaviour
{
    //teclas de movimentacao
    private KeyCode teclaAndarFrente = KeyCode.W;
    private KeyCode teclaAndarTras = KeyCode.S;
    private KeyCode teclaAndarEsquerda = KeyCode.A;
    private KeyCode teclaAndarDireita = KeyCode.D;
    private KeyCode teclaPular = KeyCode.Space;
    private KeyCode teclaCorrer = KeyCode.LeftShift;

    private int velocidadeAtual = 10;//velocidade atual do personagem podendo mundar se esta correndo ou nao

    public int velocidadeBase = 10;//velocidade base do personagem
    public int multiplciadorCorrer = 2;//multiplicador de correr do personagem
    public float atritoChao = 1f;//atrito com o chao
    public float atritoAr = 0.3f;//atrito com o ar, tem que ser menor, pois quando plana ele nao pode se mover mais
    public int alturaPulo = 10;//velocidade do pulo para o y
    public int speedPlanar = 1;//velocidade maxima que o personagem pode cair quando esta planando(maior valor menos o personagem plana)

    private bool estaCorrendo = false;//verifica se o personagem esta correndo
    private bool podeMover = true;//verifica se pode se mover(quando esta no meio do pulo, o personagem nao pode se mover)

    GameObject player;
    Rigidbody playerRigidBody;
    public Transform cameraParent;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        playerRigidBody = player.GetComponent<Rigidbody>();
        velocidadeAtual = velocidadeBase;
    }

    // Update is called once per frame
    void Update()
    {
        if (podeMover == true)
        {
            attVelocidade();
            if (Input.GetKeyDown(teclaPular) && playerRigidBody.velocity.y < 1 && playerRigidBody.velocity.y > -1)
            {
                podeMover = false;
                playerRigidBody.velocity = playerRigidBody.velocity + new Vector3(0, alturaPulo, 0);
                playerRigidBody.drag = atritoAr;
            }
            if (Input.GetKey(teclaAndarEsquerda))
            {
                Vector3 esquerda = cameraParent.right;//pega o lado da camera
                esquerda.y = 0;//coloca o angulo y para 0 pois somente anda no x e y
                esquerda.Normalize();//seta o angulo entre 0 e 1
                esquerda = esquerda * -1;//se for esquerda ou atras, multiplica por -1, pois ambos estao do lado oposto de right ou forward
                Vector3 velocidadeDirecao = velocidadeAtual * esquerda;//velocidade angular com a velocidade
                Vector3 velocidadeTotal = playerRigidBody.velocity + velocidadeAtual * esquerda;//total da velocidade angular + a atual
                //verificacoes para ver se o player nao esta mais rapido do que o possivel e diminui se for o caso
                if (velocidadeDirecao.x < velocidadeTotal.x && velocidadeDirecao.x > 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                else if (velocidadeDirecao.x > velocidadeTotal.x && velocidadeDirecao.x < 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                if (velocidadeDirecao.z < velocidadeTotal.z && velocidadeDirecao.z > 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                else if (velocidadeDirecao.z > velocidadeTotal.z && velocidadeDirecao.z < 0)
                    velocidadeTotal.z = velocidadeDirecao.z;

                playerRigidBody.velocity = velocidadeTotal;//passa a velocidade para o player
            }
            if (Input.GetKey(teclaAndarFrente))//mesmo que os outros, somente mudando o botao e o lado da camera
            {
                Vector3 forward = cameraParent.forward;
                forward.y = 0;
                forward.Normalize();
                Vector3 velocidadeDirecao = velocidadeAtual * forward;
                Vector3 velocidadeTotal = playerRigidBody.velocity + velocidadeAtual * forward;
                if (velocidadeDirecao.x < velocidadeTotal.x && velocidadeDirecao.x > 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                else if (velocidadeDirecao.x > velocidadeTotal.x && velocidadeDirecao.x < 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                if (velocidadeDirecao.z < velocidadeTotal.z && velocidadeDirecao.z > 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                else if (velocidadeDirecao.z > velocidadeTotal.z && velocidadeDirecao.z < 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                playerRigidBody.velocity = velocidadeTotal;

            }
            if (Input.GetKey(teclaAndarTras))//mesmo que os outros, somente mudando o botao e o lado da camera
            {
                Vector3 atras = cameraParent.forward;
                atras.y = 0;                        
                atras.Normalize();
                atras = atras * -1;
                Vector3 velocidadeDirecao = velocidadeAtual * atras;
                Vector3 velocidadeTotal = playerRigidBody.velocity + velocidadeAtual * atras;
                if (velocidadeDirecao.x < velocidadeTotal.x && velocidadeDirecao.x > 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                else if (velocidadeDirecao.x > velocidadeTotal.x && velocidadeDirecao.x < 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                if (velocidadeDirecao.z < velocidadeTotal.z && velocidadeDirecao.z > 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                else if (velocidadeDirecao.z > velocidadeTotal.z && velocidadeDirecao.z < 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                playerRigidBody.velocity = velocidadeTotal;
            }
            if (Input.GetKey(teclaAndarDireita))//mesmo que os outros, somente mudando o botao e o lado da camera
            {
                Vector3 direita = cameraParent.right;
                direita.y = 0;
                direita.Normalize();
                Vector3 velocidadeDirecao = velocidadeAtual * direita;
                Vector3 velocidadeTotal = playerRigidBody.velocity + velocidadeAtual * direita;
                if (velocidadeDirecao.x < velocidadeTotal.x && velocidadeDirecao.x > 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                else if (velocidadeDirecao.x > velocidadeTotal.x && velocidadeDirecao.x < 0)
                    velocidadeTotal.x = velocidadeDirecao.x;
                if (velocidadeDirecao.z < velocidadeTotal.z && velocidadeDirecao.z > 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                else if (velocidadeDirecao.z > velocidadeTotal.z && velocidadeDirecao.z < 0)
                    velocidadeTotal.z = velocidadeDirecao.z;
                playerRigidBody.velocity = velocidadeTotal;

            }


        }
        else
        {
            if (Input.GetKey(teclaPular) && playerRigidBody.velocity.y < 0)//verifica se esta segurando o botao de pulo para assim planar
            {
                if(playerRigidBody.velocity.y < - speedPlanar)
                    playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, - speedPlanar, playerRigidBody.velocity.z);
            }
        }
        

    }

    private void attVelocidade()//muda se esta apertando a tecla de correr para atualizar a velocidade
    {
        if (Input.GetKey(teclaCorrer) && estaCorrendo == false)
        {
            velocidadeAtual = velocidadeBase * multiplciadorCorrer;
            estaCorrendo = true;
        }

        if (Input.GetKeyUp(teclaCorrer) && estaCorrendo == true)
        {
            velocidadeAtual = velocidadeBase;
            estaCorrendo = false;
        }
    }

    void OnTriggerEnter(Collider other)//verifica se o objeto chegou no chao para poder pular e andar denovo
    {
        if (other.gameObject.tag == "Chao" && podeMover == false)
        {
            playerRigidBody.drag = atritoChao;
            podeMover = true;
        }
            
    }
}
