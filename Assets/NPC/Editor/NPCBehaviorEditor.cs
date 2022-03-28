using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;
using TMPro;

[CustomEditor(typeof(NPCBehavior), true)]
[CanEditMultipleObjects]
public class NPCBehaviorEditor : BaseAIEditor
{
    protected override void Inspector()
    {

	    var isNPCBehavior = typeof(NPCBehavior).IsAssignableFrom(target.GetType());
        

        Section("NPC");
        //PropertyField(FindProperty("paths"));
        PropertyField(FindProperty("ActivityGroups"));
        PropertyField(FindProperty("speechObject"));
        PropertyField(FindProperty("Name"));
        PropertyField(FindProperty("PositionName"));
        PropertyField(FindProperty("Value"));
        PropertyField(FindProperty("ManipulationLevel"));
        PropertyField(FindProperty("CharacterBehavior"));
        PropertyField(FindProperty("WaypointPrefab"));
        base.Inspector();
		//ObjectField("paths", min: 0.01f);
		//ObjectField("Activites", min: 0.01f);
    }
}
