using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonaTimer))]
public class MonaTimerEditor : Editor
{
    GUIStyle backgroundStyle;
    int textSize = 14;
    int buttonSpace = 6;

    Color buttonBackgroundColor = new Color(1.0f, 0.0f, 1.0f);
    Color buttonTextColor = new Color(1.0f, 1.0f, 1.0f);

    private void OnEnable()
    {
        backgroundStyle = new GUIStyle();
        backgroundStyle.normal.background = MakeTex(2, 2, Color.black);
    }

    public override void OnInspectorGUI()
    {
        MonaTimer MonaTimer = (MonaTimer)target;
        GUILayout.BeginVertical(backgroundStyle);

        if (!string.IsNullOrEmpty(MonaTimer.imagePath))
        {
            Texture2D myImage = AssetDatabase.LoadAssetAtPath<Texture2D>(MonaTimer.imagePath);
            if (myImage != null)
            {
                float inspectorWidth = EditorGUIUtility.currentViewWidth;
                float aspectRatio = (float)myImage.width / myImage.height;

                // Limit the image width to the smaller of: the inspector width, 400 or the image's actual width
                float imageWidth = Mathf.Min(inspectorWidth, 400, myImage.width);

                // Using the calculated width and the aspect ratio, determine the height
                float imageHeight = imageWidth / aspectRatio;

                Rect rect = GUILayoutUtility.GetRect(imageWidth, imageHeight);
                GUI.DrawTexture(rect, myImage, ScaleMode.ScaleToFit);
            }
            else
            {
                EditorGUILayout.LabelField("Image not found at path:", MonaTimer.imagePath);
            }
        }

        // CREDITS STYLE

        GUIStyle textStyle = new GUIStyle(GUI.skin.label);
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = textSize;

        // CREDITS TEXT

        EditorGUILayout.LabelField(MonaTimer.ModuleName, textStyle, GUILayout.Height(textSize + 4));
        EditorGUILayout.LabelField(MonaTimer.ModuleCreator, textStyle, GUILayout.Height(textSize + 4));

        // BUTTONS STYLE

        GUIStyle hyperlinkTextStyle = new GUIStyle(GUI.skin.button);
        hyperlinkTextStyle.normal.textColor = buttonTextColor;
        hyperlinkTextStyle.fontSize = 12;
        hyperlinkTextStyle.alignment = TextAnchor.MiddleCenter;

        // BUTTONS

        if (GUILayout.Button(MonaTimer.ButtonTitle1, hyperlinkTextStyle, GUILayout.Height(textSize + buttonSpace)))
        {
            Application.OpenURL(MonaTimer.Link1);
        }

        GUILayout.Space(1);

        if (GUILayout.Button(MonaTimer.ButtonTitle2, hyperlinkTextStyle, GUILayout.Height(textSize + buttonSpace)))
        {
            Application.OpenURL(MonaTimer.Link2);
        }

        GUILayout.Space(1);

        if (GUILayout.Button(MonaTimer.ButtonTitle3, hyperlinkTextStyle, GUILayout.Height(textSize + buttonSpace)))
        {
            Application.OpenURL(MonaTimer.Link3);
        }

        GUILayout.EndVertical();

        // EXTERIOR BUTTONS

        //GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        //buttonStyle.normal.textColor = buttonTextColor;
        //buttonStyle.normal.background = MakeTex(2, 2, buttonBackgroundColor);

        //if (GUILayout.Button("TUTORIAL VIDEO", buttonStyle))
        //{
        //    MonaTimer.OpenURL();
        //}

        //GUILayout.Space(3);

        //if (GUILayout.Button("TUTORIAL VIDEO", buttonStyle))
        //{
        //    MonaTimer.OpenURL();
        //}
 
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
