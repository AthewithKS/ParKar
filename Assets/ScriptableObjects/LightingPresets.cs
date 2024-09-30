using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Lighting Preset",menuName ="scriptable/Lighting Preset",order =1)]
public class LightingPresets : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient fogcolor;
}
