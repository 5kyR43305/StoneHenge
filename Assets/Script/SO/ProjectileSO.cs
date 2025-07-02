using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    public float Speed;
    public float Angle;
    public float Mass;
}
