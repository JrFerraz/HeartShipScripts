using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoDamages : MonoBehaviour
{
    public static float[] standardAmmo(bool hasShield, bool hasArmor, float dmg, float vida, float escudo, float armadura, string typeAmmo)
    {
        float dmgMultiple = 1;

        if(typeAmmo.Equals("Ammo2")) dmgMultiple = 0.5f;

        if (hasShield && hasArmor)
        {
            escudo -= (dmg * dmgMultiple);
            if (escudo < 0)
            {
                armadura += escudo;
                escudo = 0;
            }
            if (armadura < 0)
            {
                vida += armadura;
                armadura = 0;
            }
        }

        if (!hasShield && hasArmor)
        {
            armadura -= dmg;
            if (armadura < 0)
            {
                vida += armadura;
                armadura = 0;
            }
        }

        if (hasShield && !hasArmor)
        {
            escudo -= (dmg * dmgMultiple);
            if (escudo < 0)
            {
                vida += escudo;
                escudo = 0;
            }
        }
        if (!hasArmor && !hasShield)
        {
            vida -= (dmg * dmgMultiple);
        }
        float[] vidaRestante = {vida, armadura, escudo};
        return vidaRestante;
    }
}
