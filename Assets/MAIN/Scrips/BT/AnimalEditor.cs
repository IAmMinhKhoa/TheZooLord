#if UNITY_EDITOR // => Ignore from here to next endif if not in editor
using UnityEditor;
#endif
using UnityEngine;

public class AnimalEditor 
{
  /*  //Store a reference to the instance of the class
    private AnimalController m_NPC;

    private void OnEnable()
    {
        //Get the reference to the instance of the class
        m_NPC = target as AnimalController;

        EditorApplication.update += RedrawView;

       
    }

    void RedrawView()
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        //Makes sure the original inspector is drawn. Without this the button would replace any serialized properties
        DrawDefaultInspector();

        //Create the button
        if (GUILayout.Button("Draw Behavior Tree"))
        {
            //The method that is run if the button is pushed
            m_NPC.ForceDrawingOfTree();
        }
    }*/
}
