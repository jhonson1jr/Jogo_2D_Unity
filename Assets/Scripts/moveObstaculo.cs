using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObstaculo : MonoBehaviour {

    public float velocidade;//Variavel para controlar a velocidade do objeto
    private float eixoX; //armazenar os valores do eixo X do objeto obstaculo
    public GameObject Player;
    private bool pontuado;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player") as GameObject; //Pegando as propriedades do Personagem ao instanciar um novo Obstaculo
	}
	
	// Update is called once per frame
	void Update () {
        eixoX = transform.position.x; //pegando a posicao do eixo X atual
        //Multiplicando a velocidade pelo tempo de execução de cada frame garante q a velocidade seja a mesma em diferentes fps:
        eixoX += velocidade + Time.deltaTime;

        //aplicando a nova posicao do X no objeto:
        transform.position = new Vector3(eixoX, transform.position.y, transform.position.z);
        if(eixoX <= -5.5)
        {
            Destroy(transform.gameObject); //Destruindo o Objeto Obstaculo quando ele sai da tela
        }

        //Controlando a exibição da pontuação
        if(eixoX < Player.transform.position.x && pontuado == false)
        {
            pontuado = true; //para garantir que nao vai incrementar mais a pontuacao enquanto esse objeto existir
            PlayerController.pontuacao += 1; //Incrementando a variavel static que manuseia a pontuacao no arquivo PlayerController

        }
	}
}
