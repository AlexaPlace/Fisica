using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controlador : MonoBehaviour
{

    [SerializeField]
    GameObject C2;
    [SerializeField]
    GameObject C3;
    [SerializeField]
    Button Anterior;
    [SerializeField]
    Button Siguiente;
    [SerializeField]
    Button BotonRojo;
    [SerializeField]
    GameObject Alerta;
    [SerializeField]
    TextMeshProUGUI TextoAlerta;

    [SerializeField]
    float PosFinal;

    [Header("Variables")]
    [SerializeField]
    float TiempoDeBajada;
    [SerializeField]
    float VelocidadDeGiro;
    [SerializeField]
    int PosTexto = 0;
    [SerializeField]
    TextMeshProUGUI Txt;

    [Header("Variables de textura")]
    [SerializeField]
    float TiempoColor;
    [SerializeField]
    float TiempoBrillo;
    [SerializeField]
    float TiempoSoldadura;
    [SerializeField]
    float TiempoDeEspera;

    bool AumentandoVelocidad = false;
    bool AumentandoExceso = false;
    bool Interc = false;
    public bool Apagado = false;
    bool Comenzo = false;
    float TiempoGirando = 0;
    public float ContApagado = 1;
    public float SegundosAlerta;

    private void Start()
    {
        C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Color", 0);
        C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Luminosidad", 0);
        C3.transform.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Ex", 1);
    }

    void Update()
    {
        if (Interc == false && SegundosAlerta>0)
        {
            StartCoroutine(Intercalar());
        }

        if (Comenzo && SegundosAlerta>0)
        {
            SegundosAlerta -= Time.deltaTime;
            TextoAlerta.text = ((int)SegundosAlerta).ToString();
        }
        else
        {
            Alerta.SetActive(false);
        }


        if (PosTexto <= 0)
        {
            PosTexto = 0;
            Anterior.interactable = false;
        }
        else
        {
            if (!Comenzo)
            {
                Anterior.interactable = true;

            }

        }

        if (PosTexto >= 11)
        {
            PosTexto = 11;
            Siguiente.interactable = false;
        }
        else
        {
            Siguiente.interactable = true;

        }

        if (!Comenzo)
        {
            switch (PosTexto)
            {
                default:
                    {
                        BotonRojo.interactable = false;
                        break;
                    }

                case 0:
                    {

                        Txt.text = "Bienvenido al Simulador de Soldadura, a continuación, una breve explicación de lo que verás en los próximos 5 minutos:";
                        break;
                    }

                case 1:
                    {
                        Txt.text = "La Soldadura por fricción usa la fricción para unir materiales, incluso cuando estos son distintos, existen distintos tipos de soldadura por fricción, pero usaremos la Rotativa.";
                        break;
                    }

                case 2:
                    {
                        Txt.text = "En esta, se consigue rotando y aplicando presión extrema a un material hasta que dar con un efecto de plasticidad";
                        break;
                    }

                case 3:
                    {
                        Txt.text = "Hay muchas ventajas que ofrece este tipo de Soldadura, para empezar, es el único proceso que puede lograr una unión del 100% de metal con metal";
                        break;
                    }

                case 4:
                    {
                        Txt.text = "Además de que no se necesita material adicional y no se producen emisiones";
                        break;
                    }

                case 5:
                    {
                        Txt.text = "Debido a esto, se ha convertido un estándar en la Industria, entre las aplicaciones más destacadas se encuentra su uso en la Industria Aeronáutica y Espacial";
                        break;
                    }

                case 6:
                    {
                        Txt.text = "La Soldadura se divide en 4 fases, no te preocupes, explicaremos con detalle cada una después, por el momento, solo las nombraremos: ";
                        break;
                    }

                case 7:
                    {
                        Txt.text = "Fase de Prefriccion";
                        break;
                    }

                case 8:
                    {
                        Txt.text = "Fase de Friccion";
                        break;
                    }

                case 9:
                    {
                        Txt.text = "Fase de Recalcado";
                        break;
                    }

                case 10:
                    {
                        BotonRojo.interactable = false;
                        Txt.text = "Mantenimiento";
                        break;
                    }

                case 11:
                    {
                        BotonRojo.interactable = true;
                        Txt.text = "Ahora mismo la maquina se encuentra en fase de prefriccion, listo y alineado para comenzar\n\n Presiona el boton rojo para continuar...";
                        break;
                    }
            }

        }


        if (C2.transform.position.y == -2.65f && TiempoGirando < TiempoDeEspera)
        {
            if (AumentandoExceso)
            {

            }
            else
            {

                Txt.text = "Durante la fase de Friccion las piesas se unen a medida molecular";
            }
            Anterior.interactable = false;
            C2.transform.Rotate(new Vector3(0f, VelocidadDeGiro, 0f) * Time.deltaTime);
            TiempoGirando += Time.deltaTime;
            if (AumentandoVelocidad == false)
            {
                StartCoroutine(AumentarVelocidad());
            }
        }
        else
        {
            if (!Apagado && ContApagado > 0)
            {
                if (Comenzo)
                {

                    Txt.text = "Por ultimo se solidifica el metal y se deja enfriar la union.\n\nA estas fases se le llaman\n Recalcado y Mantenimiento";
                    StartCoroutine(Apagar());
                }
            }

        }

        if (TiempoGirando / TiempoColor < 1 && VelocidadDeGiro > 0)
        {
            C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Color", TiempoGirando / TiempoColor);

        }

        if (TiempoGirando / TiempoBrillo < 1 && VelocidadDeGiro > 0)
        {
            C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Luminosidad", TiempoGirando / TiempoBrillo);

        }
        else
        {
            if (!AumentandoExceso)
            {
                StartCoroutine(AumentarExceso());
            }
        }


    }

    public void Bajar()
    {
        BotonRojo.interactable = false;
        C2.transform.LeanMoveLocalY(PosFinal, TiempoDeBajada);
        Comenzo = true;
        Alerta.SetActive(true);
    }

    IEnumerator AumentarVelocidad()
    {
        AumentandoVelocidad = true;
        for (int i = 1; i <= 100; i++)
        {
            VelocidadDeGiro = i * 10;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator AumentarExceso()
    {
        AumentandoExceso = true;
        C3.LeanScale(new Vector3(3.3f, 0.5f, 4), 5);
        yield return new WaitForSeconds(5);
    }

    IEnumerator Apagar()
    {
        Apagado = true;
        ContApagado -= 0.005f;
        C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Color", ContApagado);
        C2.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Luminosidad", ContApagado);
        C3.transform.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Ex", ContApagado);
        yield return new WaitForSeconds(0.1f);
        Apagado = false;
    }

    public void BSiguiente()
    {

        PosTexto++;
    }
    public void BAnterior()
    {
        PosTexto--;
    }

    IEnumerator Intercalar()
    {
        Interc = true;

        if (Alerta.GetComponent<Image>().color.a == 1)
        {
            Alerta.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        else
        {
            Alerta.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        yield return new WaitForSeconds(1);
        Interc = false;
    }
}
