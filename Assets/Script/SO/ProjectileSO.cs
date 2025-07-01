using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    public int Speed;
    public int Angle;
    public int Mass;
}
