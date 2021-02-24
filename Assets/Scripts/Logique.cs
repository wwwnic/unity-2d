using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Logique : MonoBehaviour
{
    public static bool FonctionEt(bool a, bool b)
    {
        return a && b;
    }

    public static bool FonctionOu(bool a, bool b)
    {
        return a || b;
    }

    public static bool FonctionNon(bool a)
    {
        return !a;
    }

    public static bool FonctionNonEt(bool a, bool b)
    {
        return !(a && b);
    }

    public static bool FonctionNonOu(bool a, bool b)
    {
       return !(a || b);
    }

    public static bool FonctionOuExclusif(bool a, bool b)
    {
        return !(a && b || !a && !b);
    }


    public static bool FonctionNonOuExclusif(bool a, bool b)
    {
        return a && b || !a && !b;
    }
}


