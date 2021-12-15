using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistant
{
    string Read();
    void Load(string jsonString);
}
