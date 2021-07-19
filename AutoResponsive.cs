using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class AutoResponsive : EditorWindow
{
    private static bool m_toggleSwitch = false;
    private static GameObject m_saveTarget;
    private static string m_updateText = "plugin is disable...";
    private static Texture2D m_tex;
    private static Color m_backgroundColor = new Color32(50, 180, 140, 255);

    [MenuItem("Window/Auto Responsive UI")]
    private static void ShowWindow()
    {
        AutoResponsive _window = (AutoResponsive)GetWindowWithRect(typeof(AutoResponsive), new Rect(0, 0, 230, 70));
        _window.titleContent = new GUIContent("Auto Responsive UI");

        m_tex = new Texture2D(1, 1) ;
        m_tex.SetPixel(0, 0, m_backgroundColor);
        m_tex.Apply();

        _window.Show();
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), m_tex, ScaleMode.StretchToFill);

        var _fontStyle = EditorStyles.label.fontStyle;
        EditorStyles.label.fontStyle = FontStyle.Bold;
        GUILayout.Label("Check box for activate Responsive UI", EditorStyles.label);
        EditorStyles.label.fontStyle = _fontStyle;
        GUILayout.Label("");

        m_toggleSwitch = GUILayout.Toggle(m_toggleSwitch, m_updateText);    
    }

    private void OnDestroy()
    {
        m_toggleSwitch = false;
    }

    private void Update()
    {
        SwitchState();   
    }

    private static void SwitchState()
    {
        if (m_toggleSwitch)
        {
            if (m_saveTarget == null)
            {
                Debug.Log("bob");
            }

            var _selectGO = Selection.activeGameObject;
            Debug.Log("AutoResponsive UI activate");
            m_updateText = "plugin is enable !";

            if(_selectGO != null)
            {
                m_saveTarget = _selectGO;
            }
            else
            {
                StartResponsiveUI(m_saveTarget);
            }  
        }
        else
        {
            Debug.Log("Desactivate");
            m_updateText = "plugin is disable...";
        }
    }

    private static void StartResponsiveUI(GameObject gameObject)
    {
        var _selectGameobject = gameObject.GetComponent<RectTransform>();
        var _mainTransform = gameObject.transform.parent.GetComponent<RectTransform>();

        Vector2 offsetMin = _selectGameobject.offsetMin;
        Vector2 offsetMax = _selectGameobject.offsetMax;
        Vector2 anchorMin = _selectGameobject.anchorMin;
        Vector2 anchorMax = _selectGameobject.anchorMax;

        Vector2 _mainTransformScale = new Vector2(_mainTransform.rect.width, _mainTransform.rect.height);
        _selectGameobject.anchorMin = new Vector2(anchorMin.x + (offsetMin.x / _mainTransformScale.x), anchorMin.y + (offsetMin.y / _mainTransformScale.y));
        _selectGameobject.anchorMax = new Vector2(anchorMax.x + (offsetMax.x / _mainTransformScale.x), anchorMax.y + (offsetMax.y / _mainTransformScale.y));

        _selectGameobject.offsetMin = Vector2.zero;
        _selectGameobject.offsetMax = Vector2.zero;
    }
}
#endif