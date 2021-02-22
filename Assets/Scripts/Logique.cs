using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Logique : MonoBehaviour
{
    public static bool FonctionEt(bool a, bool b)
    {
        if (a == true && b == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool FonctionOu(bool a, bool b)
    {
        if (a == true || b == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool FonctionNon(bool a)
    {
        return !a;
    }

    public static bool FonctionNonEt(bool a, bool b)
    {
        if (a == false && b == false)
        {
            return true;
        }
        else if ((a == false && b == true) || (a == true && b == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool FonctionNonOu(bool a, bool b)
    {
        if (a == false && b == false)
        {
            return true;
        }
        else if ((a == false && b == true) || (a == true && b == false))
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool FonctionOuExclusif(bool a, bool b)
    {
        if (a == false && b == false)
        {
            return false;
        }
        else if ((a == false && b == true) || (a == true && b == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


