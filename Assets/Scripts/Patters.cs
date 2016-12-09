using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Patters {
    public static Regex gril1 = new Regex(@"\b(girl1:)\s*");
    public static Regex regDOWN = new Regex(@"\b(down)\s*(\d+)?");
    public static Regex regLEFT = new Regex(@"\b(left)\s*(\d+)?");
    public static Regex regRIGHT = new Regex(@"\b(right)\s*(\d+)?");
    public static Regex regNEWMAP = new Regex(@"\b(newMap)\s*(\d+)\s*(\d+)");
    public static Regex regSaveMAP = new Regex(@"\b(saveMap)");
}
