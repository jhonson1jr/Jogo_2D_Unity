using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaBarreirasController : MonoBehaviour {

    //Variaveis que vao receber os Objetos a instanciar dinamicamente
    public GameObject barreira; //Objeto a ser criado dinamicamente

    public float intervaloObjetos;
    private float contadorTempo;

    private int posicao;
    private float posicaoEixoY;
    public float posicaoA; // Objeto A
    public float posicaoB; // Objeto B


    // Use this for initialization
    void Start () {
        contadorTempo = 0; //Instanciamos o objeto com tempo (contador) zerado
	}
	
	// Update is called once per frame
	void Update () {
        contadorTempo += Time.deltaTime; //Acrescentando o tempo percorrido da execução dos frames ao contador
        if(contadorTempo >= intervaloObjetos) //Se o contador atingiu o tempo para instanciação de objetos, criamos:
        {
            contadorTempo = 0;
            if(Random.Range(0,100) < 50)
            {
                posicaoEixoY = posicaoA;
            }
            else
            {
                posicaoEixoY = posicaoB;
            }
            //Criando um objeto temporario e instanciando como GameObject (as GameObject)
            GameObject objTemporarioChao = Instantiate(barreira) as GameObject;
            //Garantindo q os obj temporarios serao criados a partir do Obj CriaBarreiras:
            objTemporarioChao.transform.position = new Vector3(transform.position.x, posicaoEixoY, transform.position.z);
        }
    }
}
