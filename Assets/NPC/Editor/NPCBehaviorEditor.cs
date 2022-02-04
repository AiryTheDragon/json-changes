using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;

[CustomEditor(typeof(NPCBehavior), true)]
[CanEditMultipleObjects]
public class NPCBehaviorEditor : BaseAIEditor
{
    protected override void Inspector()
    {

	    var isNPCBehavior = typeof(NPCBehavior).IsAssignableFrom(target.GetType());
        

        Section("NPC");
        //PropertyField(FindProperty("paths"));
        PropertyField(FindProperty("Activities"));
        base.Inspector();
		//ObjectField("paths", min: 0.01f);
		//ObjectField("Activites", min: 0.01f);
    }
}
