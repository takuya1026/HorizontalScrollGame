using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneParameter<Parameter>
{
    void Initialize(Parameter parameter); 
}
