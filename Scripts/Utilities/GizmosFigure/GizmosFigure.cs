using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmosFigure : MonoBehaviour
{
    public FigureType figureType = FigureType.Cube;

    [SerializeField] bool _isGizmosEnabled;
    [SerializeField, Space] Color _color;

    [HideInInspector] public Vector3 cubeScale;

    [HideInInspector] public float sphereRadius;

    [HideInInspector] public Vector3 lineFrom;
    [HideInInspector] public Vector3 lineTo;

    public enum FigureType
    {
        Cube, WireCube, Sphere, WireSphere, Line
    }

    private void OnDrawGizmos()
    {
        if (!_isGizmosEnabled) return;

        Gizmos.color = _color;

        switch (figureType)
        {
            case FigureType.Cube:
                Gizmos.DrawCube(transform.position, cubeScale);
                break;
            case FigureType.WireCube:
                Gizmos.DrawWireCube(transform.position, cubeScale);
                break;
            case FigureType.Sphere:
                Gizmos.DrawSphere(transform.position, sphereRadius);
                break;
            case FigureType.WireSphere:
                Gizmos.DrawWireSphere(transform.position, sphereRadius);
                break;
            case FigureType.Line:
                Gizmos.DrawLine(transform.TransformPoint(lineFrom), transform.TransformPoint(lineTo));
                break;
            default:
                break;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GizmosFigure)), CanEditMultipleObjects]
public class GizmosFigureEditor : Editor
{
    GizmosFigure gizmosFigure;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        gizmosFigure = target as GizmosFigure;

        switch (gizmosFigure.figureType)
        {
            case GizmosFigure.FigureType.Cube:
            case GizmosFigure.FigureType.WireCube:
                gizmosFigure.cubeScale = EditorGUILayout.Vector3Field("Cube scale", gizmosFigure.cubeScale);
                break;
            case GizmosFigure.FigureType.Sphere:
            case GizmosFigure.FigureType.WireSphere:
                gizmosFigure.sphereRadius = EditorGUILayout.FloatField("Sphere radius", gizmosFigure.sphereRadius);
                break;
            case GizmosFigure.FigureType.Line:
                gizmosFigure.lineFrom = EditorGUILayout.Vector3Field("Start point", gizmosFigure.lineFrom);
                gizmosFigure.lineTo = EditorGUILayout.Vector3Field("End point", gizmosFigure.lineTo);
                break;
            default:
                break;
        }
    }
} 
#endif