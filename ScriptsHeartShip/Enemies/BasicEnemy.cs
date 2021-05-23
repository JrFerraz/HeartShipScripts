
using UnityEngine;

[System.Serializable]
public class BasicEnemy : MonoBehaviour
{
    public Enemy statsEnemigo;

    private void Awake()
    {
        if (gameObject.GetComponent<Verdin>() != null) statsEnemigo = new Enemy(10f, 0f, 0f, 5f);
        if (gameObject.GetComponent<MarcianitoExplosivo>() != null) statsEnemigo = new Enemy(40f, 10f, 0f, 35f);
    }

    public class Enemy
    {
        public float vidaMax;
        public float armaduraMax;
        public float escudoMax;

        public float escudo;
        public float vida;
        public float armadura;

        public float dmg;

        public bool stunned;

        public Enemy(float v, float a, float e, float d)
        {
            vidaMax = v;
            armaduraMax = a;
            escudoMax = e;
            vida = vidaMax;
            armadura = armaduraMax;
            escudo = escudoMax;
            dmg = d;
            stunned = true;
        }
    }

}
