using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GOALS{
    POSTA,
    ALMACEN,
    ARCADE,
    MOTEL,
    CEMENTERIO
};

public class Roads
{
    public int luz;
    public GOALS road_goal;
    public int peligro;
}
