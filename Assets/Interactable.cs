using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum Type
    {
        StartLevel1,
        StartLevel2,
        EndLevel,
        Collectible,
    }

    public Type type;
}
