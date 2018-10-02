using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffsetComponente : MonoBehaviour {
    private Material materialAtual; //Pegar o material que estamos trabalhando
    public float velocidade; //velocidade que o offset vai mudar
    private float offset; //Offset pêgo do material

	// Use this for initialization
	void Start () {
        materialAtual = GetComponent<Renderer>().material; //Pegando o material que está vinculado ao script
	}
	
	// Update is called once per frame
	void Update () {
        offset += velocidade * Time.deltaTime; //Para garantir que a velocidade seja a mesma independente da taxa de frames do PC

        //Aplicando a velocidade no componente:
        materialAtual.SetTextureOffset("_MainTex", new Vector2(offset, 0)); // movimentando o material somente no eixo X
	}
}
