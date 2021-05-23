using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puntuaciones : MonoBehaviour
{
    public static int DineroMaximo;
    public static int puntos;
    public static int mayorRacha;
    public static int DineroGastado;
    public static int total;

    private void Awake()
    {
        DineroGastado = 0;
        DineroMaximo = 0;
    }

    public static void limpiarPuntuaciones()
    {
        PlayerPrefs.SetInt("Oro", 0);
        PlayerPrefs.SetInt("Plata", 0);
        PlayerPrefs.SetInt("Bronce", 0);

        PlayerPrefs.SetInt("puntosOro", 0);
        PlayerPrefs.SetInt("puntosPlata", 0);
        PlayerPrefs.SetInt("puntosBronce", 0);

        PlayerPrefs.SetInt("rachaOro", 0);
        PlayerPrefs.SetInt("rachaPlata", 0);
        PlayerPrefs.SetInt("rachaBronce", 0);

        PlayerPrefs.SetInt("dineroOro", 0);
        PlayerPrefs.SetInt("dineroPlata", 0);
        PlayerPrefs.SetInt("dineroBronce", 0);

        PlayerPrefs.SetInt("dinGastadoOro", 0);
        PlayerPrefs.SetInt("dinGastadoPlata", 0);
        PlayerPrefs.SetInt("dinGastadoBronce", 0);
    }
    
    public static void listarPuntuaciones(int puntuacionGame)
    {
        List<mejoresPuntuaciones> bestGames = new List<mejoresPuntuaciones>();

        bestGames.Add( new mejoresPuntuaciones(PlayerPrefs.GetInt("Oro", 0), PlayerPrefs.GetInt("puntosOro", 0), PlayerPrefs.GetInt("rachaOro", 0), PlayerPrefs.GetInt("dineroOro",0), PlayerPrefs.GetInt("dinGastadoOro",0)));
        bestGames.Add( new mejoresPuntuaciones(PlayerPrefs.GetInt("Plata", 0), PlayerPrefs.GetInt("puntosPlata", 0), PlayerPrefs.GetInt("rachaPlata", 0), PlayerPrefs.GetInt("dineroPlata",0), PlayerPrefs.GetInt("dinGastadoPlata",0)));
        bestGames.Add( new mejoresPuntuaciones(PlayerPrefs.GetInt("Bronce", 0), PlayerPrefs.GetInt("puntosBronce", 0), PlayerPrefs.GetInt("rachaBronce", 0), PlayerPrefs.GetInt("dineroBronce", 0), PlayerPrefs.GetInt("dinGastadoBronce",0)));
        bestGames.Add( new mejoresPuntuaciones(puntuacionGame, puntos, mayorRacha, DineroMaximo, DineroGastado));
        
        List<mejoresPuntuaciones> gamesInOrder = bestGames.OrderByDescending(x => x.puntuacionTotal).ToList();

        PlayerPrefs.SetInt("Oro", gamesInOrder[0].puntuacionTotal);
        PlayerPrefs.SetInt("Plata", gamesInOrder[1].puntuacionTotal);
        PlayerPrefs.SetInt("Bronce", gamesInOrder[2].puntuacionTotal);

        PlayerPrefs.SetInt("puntosOro", gamesInOrder[0].puntos);
        PlayerPrefs.SetInt("puntosPlata", gamesInOrder[1].puntos);
        PlayerPrefs.SetInt("puntosBronce", gamesInOrder[2].puntos);

        PlayerPrefs.SetInt("rachaOro", gamesInOrder[0].mayorRacha);
        PlayerPrefs.SetInt("rachaPlata", gamesInOrder[1].mayorRacha);
        PlayerPrefs.SetInt("rachaBronce", gamesInOrder[2].mayorRacha);

        PlayerPrefs.SetInt("dineroOro", gamesInOrder[0].dineroTotal);
        PlayerPrefs.SetInt("dineroPlata", gamesInOrder[1].dineroTotal);
        PlayerPrefs.SetInt("dineroBronce", gamesInOrder[2].dineroTotal);

        PlayerPrefs.SetInt("dinGastadoOro", gamesInOrder[0].dineroGastado);
        PlayerPrefs.SetInt("dinGastadoPlata", gamesInOrder[1].dineroGastado);
        PlayerPrefs.SetInt("dinGastadoBronce", gamesInOrder[2].dineroGastado);

        PlayerPrefs.Save();
    }


    public class mejoresPuntuaciones
    {
        public int puntuacionTotal;
        public int puntos;
        public int mayorRacha;
        public int dineroTotal;
        public int dineroGastado;

        public mejoresPuntuaciones(int pTotal, int p, int rach, int din, int dinG)
        {
            puntuacionTotal = pTotal;
            puntos = p;
            mayorRacha = rach;
            dineroTotal = din;
            DineroGastado = dinG;
        }
    }
}
