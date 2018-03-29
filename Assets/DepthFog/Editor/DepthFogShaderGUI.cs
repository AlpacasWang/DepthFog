using UnityEngine;
using UnityEditor;
using System;

public class DepthFogShaderGUI : ShaderGUI
{
    string[] options = {"Gaussian","Linear","Exp","Exp2" };
    int optionIndex = 0;
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // render the default gui
        base.OnGUI(materialEditor, properties);

        Material targetMat = materialEditor.target as Material;

        optionIndex = EditorGUILayout.Popup(optionIndex, options);
        {
            switch (optionIndex)
            {
                case 0:
                    targetMat.EnableKeyword("GAUSSIAN_ON");
                    targetMat.DisableKeyword("LINEAR_ON");
                    targetMat.DisableKeyword("EXP_ON");
                    targetMat.DisableKeyword("EXP2_ON");
                    DrawProperty(materialEditor,properties,"_FogColor");
                    DrawProperty(materialEditor, properties, "_Focus");
                    DrawProperty(materialEditor, properties, "_Rate");
                    DrawProperty(materialEditor, properties, "_Scale");
                    break;
                case 1:
                    targetMat.DisableKeyword("GAUSSIAN_ON");
                    targetMat.EnableKeyword("LINEAR_ON");
                    targetMat.DisableKeyword("EXP_ON");
                    targetMat.DisableKeyword("EXP2_ON");
                    DrawProperty(materialEditor, properties, "_LinearDensity");

                    break;
                case 2:
                    targetMat.DisableKeyword("GAUSSIAN_ON");
                    targetMat.DisableKeyword("LINEAR_ON");
                    targetMat.EnableKeyword("EXP_ON");
                    targetMat.DisableKeyword("EXP2_ON");
                    DrawProperty(materialEditor, properties, "_EXPDensity");
                    break;
                case 3:
                    targetMat.DisableKeyword("GAUSSIAN_ON");
                    targetMat.DisableKeyword("LINEAR_ON");
                    targetMat.DisableKeyword("EXP_ON");
                    targetMat.EnableKeyword("EXP2_ON");
                    DrawProperty(materialEditor, properties, "_EXP2Density");
                    break;
                default:
                    targetMat.DisableKeyword("GAUSSIAN_ON");
                    targetMat.DisableKeyword("LINEAR_ON");
                    targetMat.DisableKeyword("EXP_ON");
                    targetMat.DisableKeyword("EXP2_ON");
                    break;
            }
        }
    }

    private void DrawProperty(MaterialEditor materialEditor, MaterialProperty[] properties,string propertyName)
    {
        var property = FindProperty(propertyName, properties,true);
        materialEditor.ShaderProperty(property , property.displayName);
    }

}
