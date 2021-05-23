using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueLaser : MonoBehaviour
{
    public LineRenderer laserD;
    public LineRenderer laserI;

    public Transform rielDerecho;
    public Transform rielIzquierdo;

    public GameObject laserImpactD;
    public GameObject laserImpactI;


    public GameObject laserOutD;
    public GameObject laserOutI;

    private bool laserIsPlaying;

    private List<LayerMask> ignorar;

    private bool enabledSound = false;

    RaycastHit2D hitD;
    RaycastHit2D hitI;

    RaycastHit2D hit1D;
    RaycastHit2D hit2D;
    RaycastHit2D hit3D;

    RaycastHit2D hit1I;
    RaycastHit2D hit2I;
    RaycastHit2D hit3I;

    private List<RaycastHit2D> rayosReboteD;
    private List<RaycastHit2D> rayosReboteI;

    public List<GameObject> impactosD;
    public List<GameObject> impactosI;


    private bool disparando = false;

    private void Awake()
    {
        ignorar = new List<LayerMask>();
        rayosReboteD = new List<RaycastHit2D>();
        rayosReboteI = new List<RaycastHit2D>();

        laserIsPlaying = false;

        //Layers a ignorar por el laser
        ignores();

        colleccionRaycast();
    }
    void colleccionRaycast()
    {
        rayosReboteD.Add(hit1D);
        rayosReboteD.Add(hit2D);
        rayosReboteD.Add(hit3D);

        rayosReboteI.Add(hit1I);
        rayosReboteI.Add(hit2I);
        rayosReboteI.Add(hit3I);
    }

    void ignores()
    {
        ignorar.Add(0);
        ignorar.Add(1);
        ignorar.Add(2);
        ignorar.Add(3);
        ignorar.Add(4);
        ignorar.Add(5);
        ignorar.Add(9);
        ignorar.Add(14);
    }
    private void Start()
    {
        disableLaser();
        laserImpactD.SetActive(false);
        laserImpactI.SetActive(false);
        laserOutI.SetActive(false);
        laserOutD.SetActive(false);
        apagarImpactosD();
    }
    void encenderImpactosD()
    {
        foreach (var im in impactosD)
        {
            im.SetActive(true);
        }
    }
    void encenderImpactosI()
    {
        foreach (var im in impactosI)
        {
            im.SetActive(true);
        }
    }
    void apagarImpactosD()
    {
        foreach (var im in impactosD)
        {
            im.SetActive(false);
        }
    }
    void apagarImpactosI()
    {
        foreach (var im in impactosI)
        {
            im.SetActive(false);
        }
    }

    private void Update()
    {
        if(StatsPlayer.jugador.mun_LASER <= 0)
        {
            disableLaser();
        }

        if (Disparar.sobrecalentado)
        {
            disableLaser();
        }
        if (Disparar.sobrecalentado && StatsPlayer.jugador.munEscogida.Equals("Laser"))
        {
            Disparar.enfriarCannonCalentado();
        }
        if(!Disparar.sobrecalentado && StatsPlayer.jugador.munEscogida.Equals("Laser") && !disparando)
        {
            Disparar.enfriarCannon();
        }
        if (StatsPlayer.jugador.munEscogida.Equals("Laser") && StatsPlayer.jugador.mun_LASER != 0 && !Disparar.sobrecalentado)
        {
            if (Input.GetMouseButtonDown(0))
            {
                enableLaser();
            }

            if (Input.GetMouseButton(0))
            {
                updateLaser();
            }

            if (Input.GetMouseButtonUp(0))
            {
                disableLaser();
            }
        }

        if (InterfazGame.isPaused) disableLaser();
    }

    void disableLaser()
    {
        laserD.enabled = false;
        laserI.enabled = false;

        laserImpactD.SetActive(false);
        laserImpactI.SetActive(false);

        laserOutD.SetActive(false);
        laserOutI.SetActive(false);

        disparando = false;

        pararSonidoLaser();
        apagarImpactosD();
        apagarImpactosI();
    }

    void enableLaser()
    {
        laserD.enabled = true;
        laserI.enabled = true;

        laserOutD.SetActive(true);
        laserOutI.SetActive(true);

        disparando = true;

        encenderImpactosD();
        encenderImpactosI();
    }
    void contarColisionesD(int numHits)
    {
        if (numHits == 0) apagarImpactosD();
        else if (numHits == 1)
        {
            impactosD[1].SetActive(false);
            impactosD[2].SetActive(false);
        }
        else if (numHits == 2) impactosD[2].SetActive(false);
    }

    void contarColisionesI(int numHits)
    {
        if (numHits == 0) apagarImpactosI();
        else if (numHits == 1)
        {
            impactosI[1].SetActive(false);
            impactosI[2].SetActive(false);
        }
        else if (numHits == 2) impactosI[2].SetActive(false);
    }
    void rebotarLaserD(Vector3 position, Vector3 direction)
    {
        int posicionLine = 0;
        int numHits = 0;

        for (int i = 0; i < rayosReboteD.Count; i++)
        {
            posicionLine = i + 2;
            rayosReboteD[i] = Physics2D.Raycast(position, direction);
            Debug.DrawRay(position, direction, Color.white);
            if (rayosReboteD[i])
            {
                laserD.SetPosition(posicionLine, rayosReboteD[i].point);

                impactosD[i].SetActive(true);
                impactosD[i].transform.position = laserD.GetPosition(posicionLine);
                numHits++;
                position = rayosReboteD[i].point;
                if (rayosReboteD[i].transform.gameObject.layer == 17) direction = Vector2.Reflect(direction, rayosReboteD[i].normal);
                else direction = Vector2.zero;
            }
            else
            {
                laserD.SetPosition(posicionLine, position + direction * 25);
                if (laserD.positionCount - 1 != posicionLine) limpiarLinePointsD(posicionLine + 1, laserD.positionCount, position + direction * 25);
                contarColisionesD(numHits);
                break;
            }
            dañar(rayosReboteD[i]);
            activarInterruptor(rayosReboteD[i]);
        }
    }

    void rebotarLaserI(Vector3 position, Vector3 direction)
    {
        int posicionLine = 0;
        int numHits = 0;

        for (int i = 0; i < rayosReboteI.Count; i++)
        {
            posicionLine = i + 2;
            rayosReboteI[i] = Physics2D.Raycast(position, direction);
            Debug.DrawRay(position, direction, Color.white);
            if (rayosReboteI[i])
            {
                laserI.SetPosition(posicionLine, rayosReboteI[i].point);

                impactosI[i].SetActive(true);
                impactosI[i].transform.position = laserI.GetPosition(posicionLine);
                numHits++;
                position = rayosReboteI[i].point;
                if (rayosReboteI[i].transform.gameObject.layer == 17) direction = Vector2.Reflect(direction, rayosReboteI[i].normal);
                else direction = Vector2.zero;
            }
            else
            {
                laserI.SetPosition(posicionLine, position + direction * 25);
                if (laserI.positionCount - 1 != posicionLine) limpiarLinePointsI(posicionLine + 1, laserI.positionCount, position + direction * 25);
                contarColisionesI(numHits);
                break;
            }
            dañar(rayosReboteI[i]);
            activarInterruptor(rayosReboteI[i]);
        }
    }

    void limpiarLinePointsD(int puntoInicial, int max, Vector2 lastHit)
    {
        for (int i = puntoInicial; i < max; i++)
        {
            laserD.SetPosition(i, lastHit);
        }
    }
    void limpiarLinePointsI(int puntoInicial, int max, Vector2 lastHit)
    {
        for (int i = puntoInicial; i < max; i++)
        {
            laserI.SetPosition(i, lastHit);
        }
    }
    void desconectarI()
    {
        apagarImpactosI();
        laserI.SetPosition(2, laserI.GetPosition(1));
        laserI.SetPosition(3, laserI.GetPosition(1));
        laserI.SetPosition(4, laserI.GetPosition(1));
    }

    void desconectarD()
    {
        apagarImpactosD();
        laserD.SetPosition(2, laserD.GetPosition(1));
        laserD.SetPosition(3, laserD.GetPosition(1));
        laserD.SetPosition(4, laserD.GetPosition(1));
    }
    void updateLaser()
    {
        sonidoLaser();

        hitD = Physics2D.Raycast(rielDerecho.position, rielDerecho.up, 25f);
        hitI = Physics2D.Raycast(rielIzquierdo.position, rielIzquierdo.up, 25f);

        laserD.SetPosition(0, rielDerecho.position);
        laserI.SetPosition(0, rielIzquierdo.position);
        //!ignorar.Contains(hitI.transform.gameObject.layer)

        //if (hitI && hitI.transform.gameObject.layer == 17) rebotarLaserI(hitI.point, Vector2.Reflect(rielIzquierdo.up, hitI.normal));

        //if (hitD && hitD.transform.gameObject.layer == 17) rebotarLaserD(hitD.point, Vector2.Reflect(rielDerecho.up, hitD.normal));

        if (hitI)
        {
            if (hitI.transform.gameObject.layer == 17) rebotarLaserI(hitI.point, Vector2.Reflect(rielIzquierdo.up, hitI.normal));
            else desconectarI();
            laserI.SetPosition(1, hitI.point);
            laserImpactI.SetActive(true);
            laserImpactI.transform.position = laserI.GetPosition(1);
            dañar(hitI);
            activarInterruptor(hitI);
        }
        else
        {
            laserImpactI.SetActive(false);
            laserI.SetPosition(1, rielIzquierdo.position + rielIzquierdo.up * 25);
            desconectarI();
        }

        if (hitD)
        {
            if (hitD.transform.gameObject.layer == 17) rebotarLaserD(hitD.point, Vector2.Reflect(rielDerecho.up, hitD.normal));
            else desconectarD();
            laserD.SetPosition(1, hitD.point);
            laserImpactD.SetActive(true);
            laserImpactD.transform.position = laserD.GetPosition(1);
            dañar(hitD);
            activarInterruptor(hitD);
        }
        else
        {
            laserImpactD.SetActive(false);
            laserD.SetPosition(1, rielDerecho.position + rielDerecho.up * 25);
            desconectarD();
        }
        calentarLaser();
        consumirMunicion();

        //sI EL RAYCAST NO PILLA NADA PUEDES COGER EL FIN CON POS.INICIO + VECTOR.DIRECCION.INICIO * DISTANCIA
    }

    void calentarLaser()
    {
        Disparar.calentarCannonPorUpdate();
    }

    void consumirMunicion()
    {
        StatsPlayer.jugador.mun_LASER -= Mathf.Round(StatsPlayer.jugador.miLASER.consumoMun * Time.deltaTime);
    }

    void sonidoLaser()
    {
        if (!laserIsPlaying)
        {
            FindObjectOfType<audioManager>().Play("LaserContinuo");
            FindObjectOfType<audioManager>().Play("bgLaserContinuo");
            laserIsPlaying = true;
        }
    }

    void pararSonidoLaser()
    {
        FindObjectOfType<audioManager>().Stop("LaserContinuo");
        FindObjectOfType<audioManager>().Stop("bgLaserContinuo");
        laserIsPlaying = false;
    }

    void dañar(RaycastHit2D hit)
    {
        GameObject x = hit.transform.gameObject;
        if (x.GetComponent<BasicEnemy>() != null)
        {
            float[] barrasSalud = ammoDamages.standardAmmo(x.GetComponent<InterfazMarciana>().escudoF > 0, x.GetComponent<InterfazMarciana>().armaduraF > 0, StatsPlayer.jugador.miLASER.daño_LASER, x.GetComponent<InterfazMarciana>().vidaF, x.GetComponent<InterfazMarciana>().escudoF, x.GetComponent<InterfazMarciana>().armaduraF, x.tag);
            x.GetComponent<InterfazMarciana>().vidaF = barrasSalud[0];
            x.GetComponent<InterfazMarciana>().armaduraF = barrasSalud[1];
            x.GetComponent<InterfazMarciana>().escudoF = barrasSalud[2];

            if (x.GetComponent<InterfazMarciana>().vidaF <= 0 && !x.GetComponent<InterfazMarciana>().muerto) x.GetComponent<InterfazMarciana>().Morir();
        }

    }

    void activarInterruptor(RaycastHit2D hit)
    {
        if (hit.transform.tag.Equals("interruptor"))
        {
            hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            if (!enabledSound)
            {
                FindObjectOfType<audioManager>().Play("Aparecer");
                enabledSound = true;
            }
             
        }
    }
}
