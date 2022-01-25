using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Transitions", menuName = "ScriptableObjects/Transition Colors", order = 1)]
public class TransitionColorsScriptable : ScriptableObject
{
    public Color LightColor;
    public Color LightInmunityColor;
    public Color DarkColor;
    public Color DarkInmunityColor;
}
