using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public interface IHourObserver
{
    void onHourChanged(float hour);
}
