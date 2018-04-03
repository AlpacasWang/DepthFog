using UnityEngine;
using UnityEditor;
using System;

public class DepthFogShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // render the default gui
        base.OnGUI(materialEditor, properties);

        Material targetMat = materialEditor.target as Material;
        int optionIndex = (int)FindProperty("_TYPE", properties, true).floatValue;

        switch (optionIndex)
        {
            case 0:
                DrawProperty(materialEditor, properties, "_FogColor");
                DrawProperty(materialEditor, properties, "_Focus");
                DrawProperty(materialEditor, properties, "_Rate");
                DrawProperty(materialEditor, properties, "_Scale");
                DrawProperty(materialEditor, properties, "_Start");
                DrawProperty(materialEditor, properties, "_End");
                break;
            case 1:
                DrawProperty(materialEditor, properties, "_FogColor");
                DrawProperty(materialEditor, properties, "_LinearDensity");
                DrawProperty(materialEditor, properties, "_Start");
                DrawProperty(materialEditor, properties, "_End");
                break;
            case 2:
                DrawProperty(materialEditor, properties, "_FogColor");
                DrawProperty(materialEditor, properties, "_EXPDensity");
                DrawProperty(materialEditor, properties, "_Start");
                DrawProperty(materialEditor, properties, "_End");
                break;
            case 3:
                DrawProperty(materialEditor, properties, "_FogColor");
                DrawProperty(materialEditor, properties, "_EXP2Density");
                DrawProperty(materialEditor, properties, "_Start");
                DrawProperty(materialEditor, properties, "_End");
                break;
            default:
                DrawProperty(materialEditor, properties, "_Start");
                DrawProperty(materialEditor, properties, "_End");
                break;
        }


    }

    private void DrawProperty(MaterialEditor materialEditor, MaterialProperty[] properties, string propertyName)
    {
        var property = FindProperty(propertyName, properties, true);
        materialEditor.ShaderProperty(property, property.displayName);
    }

}
