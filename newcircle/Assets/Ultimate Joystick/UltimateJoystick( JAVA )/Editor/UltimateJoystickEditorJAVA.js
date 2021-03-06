﻿/* Written by Kaz Crowe */
/* UltimateJoystickEditorJAVA.js ver. 1.1.1 */
#pragma strict

@CustomEditor( UltimateJoystickJAVA )
public class UltimateJoystickEditorJAVA extends Editor
{
	// Warning strings
	private var tensionWarnMessage : String;
	private var tensionWarnMessageDefault : String;
	// These variables we use to store the last known option and if it's different we update it here and apply it to the joystick
	private var highlightColor : Color;
	private var tensionColor : Color;
	private var lastFadeOption : boolean;
	private var lastFadeUntouched : float;
	private var canvasRect : UnityEngine.UI.CanvasScaler;
	

	/*
	For more information on the OnInspectorGUI and adding your own variables
	in the UltimateJoystick.cs script and displaying them in this script,
	see the EditorGUILayout section in the Unity Documentation to help out.
	*/
	function OnInspectorGUI ()
	{
		// Store the joystick that we are selecting
		var joystick : UltimateJoystickJAVA = target as UltimateJoystickJAVA;
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		/* VARIABLES */
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Assigned Variables", EditorStyles.boldLabel );
		var v_option : String = "Show";
		if( EditorPrefs.GetBool( "UJ_Variables" ) == true )
			v_option = "Hide";
		if( GUILayout.Button( v_option, EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )
		{
			if( EditorPrefs.GetBool( "UJ_Variables" ) == true )
				EditorPrefs.SetBool( "UJ_Variables", false );
			else
				EditorPrefs.SetBool( "UJ_Variables", true );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		
		if( EditorPrefs.GetBool( "UJ_Variables" ) == true )
		{
			EditorGUILayout.Space();
			joystick.joystick = EditorGUILayout.ObjectField( "Joystick", joystick.joystick, RectTransform, true ) as RectTransform;
			joystick.joystickSizeFolder = EditorGUILayout.ObjectField( "Joystick Size Folder", joystick.joystickSizeFolder, RectTransform, true ) as RectTransform;
			EditorGUILayout.Space();
			if( joystick.showHighlight == true )
			{
				EditorGUILayout.LabelField( "Highlight Variables", EditorStyles.boldLabel );
				EditorGUI.indentLevel = 1;
				joystick.highlightBase = EditorGUILayout.ObjectField( "Base Highlight", joystick.highlightBase, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				joystick.highlightJoystick = EditorGUILayout.ObjectField( "Joystick Highlight", joystick.highlightJoystick, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( joystick.showTension == true )
			{
				EditorGUILayout.LabelField( "Tension Variables", EditorStyles.boldLabel );
				EditorGUI.indentLevel = 1;
				joystick.tensionAccentUp = EditorGUILayout.ObjectField( "Tension Up", joystick.tensionAccentUp, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				joystick.tensionAccentDown = EditorGUILayout.ObjectField( "Tension Down", joystick.tensionAccentDown, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				joystick.tensionAccentLeft = EditorGUILayout.ObjectField( "Tension Left", joystick.tensionAccentLeft, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				joystick.tensionAccentRight = EditorGUILayout.ObjectField( "Tension Right", joystick.tensionAccentRight, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( joystick.useAnimation == true || joystick.useFade == true )
			{
				EditorGUILayout.LabelField( "Touch Action Variables", EditorStyles.boldLabel );
				EditorGUI.indentLevel = 1;
				if( joystick.useAnimation == true )
					joystick.joystickAnimator = EditorGUILayout.ObjectField( "Animator", joystick.joystickAnimator, Animator, true ) as Animator;
				if( joystick.useFade == true )
					joystick.joystickBase = EditorGUILayout.ObjectField( "Joystick Base", joystick.joystickBase, UnityEngine.UI.Image, true ) as UnityEngine.UI.Image;
				EditorGUI.indentLevel = 0;
			}
		}
		EditorGUILayout.Space();
		
		/* SIZE AND PLACEMENT */
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Size and Placement", EditorStyles.boldLabel );
		var sap_option : String = "Show";
		if( EditorPrefs.GetBool( "UJ_SizeAndPlacement" ) == true )
			sap_option = "Hide";
		if( GUILayout.Button( sap_option, EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )//
		{
			if( EditorPrefs.GetBool( "UJ_SizeAndPlacement" ) == true )
				EditorPrefs.SetBool( "UJ_SizeAndPlacement", false );
			else
				EditorPrefs.SetBool( "UJ_SizeAndPlacement", true );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		
		if( EditorPrefs.GetBool( "UJ_SizeAndPlacement" ) == true )
		{
			EditorGUILayout.Space();
			// Arrange our joystick variables to be shown the way we want
			joystick.anchor = System.Convert.ToInt32( EditorGUILayout.EnumPopup( "Joystick Anchor", joystick.anchor ) );
			joystick.joystickTouchSize = System.Convert.ToInt32( EditorGUILayout.EnumPopup( "Joystick Touch Size", joystick.joystickTouchSize ) );
			joystick.joystickSize = EditorGUILayout.Slider( "Joystick Size", joystick.joystickSize, 1.0f, 4.0f );
			joystick.radiusModifier = EditorGUILayout.Slider( "Radius", joystick.radiusModifier, 2.0f, 7.0f );
			joystick.touchBasedPositioning = EditorGUILayout.ToggleLeft( "Touch Based Positioning", joystick.touchBasedPositioning );
			if( joystick.touchBasedPositioning == true )
			{
				EditorGUI.indentLevel = 1;
				joystick.overrideTouchSize = EditorGUILayout.Toggle( "Override Size", joystick.overrideTouchSize, EditorStyles.radioButton );
				if( joystick.overrideTouchSize == true )
				{
					EditorGUI.indentLevel = 2;
					joystick.tbp_X = EditorGUILayout.Slider( "X", joystick.tbp_X, 0.0f, 100.0f );
					joystick.tbp_Y = EditorGUILayout.Slider( "Y", joystick.tbp_Y, 0.0f, 100.0f );
					EditorGUI.indentLevel = 0;
				}
				EditorGUILayout.Space();
			}
			else
			{
				if( joystick.overrideTouchSize == true )
					joystick.overrideTouchSize = false;
			}
			EditorGUILayout.BeginVertical( "Box" );
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField( "Custom Spacing" );
			var cs_option : String = "+";
			if( EditorPrefs.GetBool( "UJ_CustomSpacing" ) == true )
				cs_option = "-";
			if( GUILayout.Button( cs_option, GUILayout.Width( 35 ), GUILayout.Height( 14f ) ) )
			{
				if( EditorPrefs.GetBool( "UJ_CustomSpacing" ) == true )
					EditorPrefs.SetBool( "UJ_CustomSpacing", false );
				else
					EditorPrefs.SetBool( "UJ_CustomSpacing", true );
			}
			GUILayout.EndHorizontal();
			if( EditorPrefs.GetBool( "UJ_CustomSpacing" ) == true )
			{
				EditorGUI.indentLevel = 1;
				EditorGUILayout.Space();
				joystick.cs_X = EditorGUILayout.Slider( "X Position:", joystick.cs_X, 0.0f, 50.0f );
				joystick.cs_Y = EditorGUILayout.Slider( "Y Position:", joystick.cs_Y, 0.0f, 100.0f );
				EditorGUILayout.Space();
				EditorGUI.indentLevel = 0;
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Space();
		
		/* STYLE AND OPTIONS */
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Style and Options", EditorStyles.boldLabel );
		var sao_option : String = "Show";
		if( EditorPrefs.GetBool( "UJ_StyleAndOptions" ) == true )
			sao_option = "Hide";
		if( GUILayout.Button( sao_option, EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )//
		{
			if( EditorPrefs.GetBool( "UJ_StyleAndOptions" ) == true )
				EditorPrefs.SetBool( "UJ_StyleAndOptions", false );
			else
				EditorPrefs.SetBool( "UJ_StyleAndOptions", true );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		
		if( EditorPrefs.GetBool( "UJ_StyleAndOptions" ) == true )
		{
			EditorGUILayout.Space();
			// TouchPad
			joystick.touchPad = EditorGUILayout.ToggleLeft( "Touch Pad", joystick.touchPad );
			if( joystick.touchPad == true )
			{
				if( joystick.showHighlight == true )
					joystick.showHighlight = false;
				if( joystick.showTension == true )
					joystick.showTension = false;
				if( joystick.joystickBase.enabled == true )
					joystick.joystickBase.enabled = false;
				if( joystick.joystick.GetComponent( UnityEngine.UI.Image ).enabled == true )
					joystick.joystick.GetComponent( UnityEngine.UI.Image ).enabled = false;

				// Check our HL and Tension
				CheckHighlights( joystick );
				CheckTensionAccents( joystick );
			}
			else
			{
				if( joystick.joystickBase.enabled == false )
					joystick.joystickBase.enabled = true;
				if( joystick.joystick.GetComponent( UnityEngine.UI.Image ).enabled == false )
					joystick.joystick.GetComponent( UnityEngine.UI.Image ).enabled = true;
			}
			
			// Throwable Joystick
			joystick.throwable = EditorGUILayout.ToggleLeft( "Throwable", joystick.throwable );
			if( joystick.throwable == true )
			{
				EditorGUI.indentLevel = 1;
				joystick.throwDuration = EditorGUILayout.Slider( "Throw Duration", joystick.throwDuration, 0.05f, 1.0f );
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( joystick.touchPad == false )
			{
				// Show Highlight
				joystick.showHighlight = EditorGUILayout.ToggleLeft( "Show Highlight", joystick.showHighlight );
				if( joystick.showHighlight == true )
				{
					// Highlight Options
					EditorGUI.indentLevel = 1;
					joystick.highlightColor = EditorGUILayout.ColorField( "Highlight Color", joystick.highlightColor );
					EditorGUI.indentLevel = 0;
					CheckHighlights( joystick );

					// If any of the variables are unassigned, we want to tell them
					if( joystick.highlightBase == null && joystick.highlightJoystick == null )
						EditorGUILayout.HelpBox( "Base and Joystick Highlight will not be displayed.", MessageType.Warning );
					else if( joystick.highlightBase == null && joystick.highlightJoystick != null )
						EditorGUILayout.HelpBox( "Base Highlight will not be displayed", MessageType.Warning );
					else if( joystick.highlightBase != null && joystick.highlightJoystick == null )
						EditorGUILayout.HelpBox( "Joystick Highlight will not be displayed", MessageType.Warning );

					EditorGUILayout.Space();
				}
				else
					CheckHighlights( joystick );
					
				// Show Tension
				joystick.showTension = EditorGUILayout.ToggleLeft( "Show Tension", joystick.showTension );
				if( joystick.showTension == true )
				{
					// Tension Options and Variables
					EditorGUI.indentLevel = 1;
					joystick.tensionColorNone = EditorGUILayout.ColorField( "Tension None", joystick.tensionColorNone );
					joystick.tensionColorFull = EditorGUILayout.ColorField( "Tension Full", joystick.tensionColorFull );
					EditorGUI.indentLevel = 0;
					CheckTensionAccents( joystick );
				}
				else
					CheckTensionAccents( joystick );
			}
			
			// This if for using constraints and boundries
			joystick.axis = System.Convert.ToInt32( EditorGUILayout.EnumPopup( "Use Axis", joystick.axis ) );
			joystick.boundary = System.Convert.ToInt32( EditorGUILayout.EnumPopup( "Use Boundary", joystick.boundary ) );
		}
		EditorGUILayout.Space();
		
		/* Touch Actions */
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Touch Actions", EditorStyles.boldLabel );
		var ta_option : String = "Show";
		if( EditorPrefs.GetBool( "UJ_TouchActions" ) == true )
			ta_option = "Hide";
		if( GUILayout.Button( ta_option, EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )//
		{
			if( EditorPrefs.GetBool( "UJ_TouchActions" ) == true )
				EditorPrefs.SetBool( "UJ_TouchActions", false );
			else
				EditorPrefs.SetBool( "UJ_TouchActions", true );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		if( EditorPrefs.GetBool( "UJ_TouchActions" ) == true )
		{
			EditorGUILayout.Space();
			// This is for calculating our taps within a time window
			joystick.tapCountOption = System.Convert.ToInt32( EditorGUILayout.EnumPopup( "Tap Count", joystick.tapCountOption ) );
			if( joystick.tapCountOption == UltimateJoystickJAVA.TapCountOption.Accumulate )
			{
				EditorGUI.indentLevel = 1;
				joystick.tapCountDuration = EditorGUILayout.Slider( "Tap Time Window", joystick.tapCountDuration, 0.0f, 1.0f );
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			
			// This is for implementing our touch actions with animations
			joystick.useAnimation = EditorGUILayout.ToggleLeft( "Use Animation", joystick.useAnimation );
			if( joystick.useAnimation == true )
			{
				if( joystick.joystickAnimator == null )
				{
					EditorGUI.indentLevel = 1;
					EditorGUILayout.HelpBox( "Joystick Animator needs to be assigned.", MessageType.Error );
					EditorGUILayout.Space();
					EditorGUI.indentLevel = 0;
				}
				if( joystick.joystickAnimator != null )
					if( joystick.joystickAnimator.enabled == false )
						joystick.joystickAnimator.enabled = true;
			}
			else
			{
				if( joystick.joystickAnimator != null )
					if( joystick.joystickAnimator.enabled == true )
						joystick.joystickAnimator.enabled = false;
			}

			// This is for implementing color fading with touch
			joystick.useFade = EditorGUILayout.ToggleLeft( "Use Fade", joystick.useFade );
			if( joystick.useFade == true )
			{
				EditorGUI.indentLevel = 1;
				joystick.fadeUntouched = EditorGUILayout.Slider( "Fade Untouched", joystick.fadeUntouched, 0.0f, 1.0f );
				joystick.fadeTouched = EditorGUILayout.Slider( "Fade Touched", joystick.fadeTouched, 0.0f, 1.0f );
				if( joystick.showTension == true )
					EditorGUILayout.HelpBox( "The alpha of Tension Color will not fade. If you want to change the alpha of the Tension Color, modify it with the Tension Color property directly.", MessageType.Warning );
				EditorGUI.indentLevel = 0;
			}
		}
		EditorGUILayout.Space();
		
		/* Resets */
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Restore To Delfault", EditorStyles.boldLabel );
		var rtd_option : String = "Show";
		if( EditorPrefs.GetBool( "UJ_RestoreToDefault" ) == true )
			rtd_option = "Hide";
		if( GUILayout.Button( rtd_option, EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )//
		{
			if( EditorPrefs.GetBool( "UJ_RestoreToDefault" ) == true )
				EditorPrefs.SetBool( "UJ_RestoreToDefault", false );
			else
				EditorPrefs.SetBool( "UJ_RestoreToDefault", true );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		
		if( EditorPrefs.GetBool( "UJ_RestoreToDefault" ) == true )
		{
			// In this section, we just are setting up hard coded values to be able to reset our options to
			EditorGUILayout.Space();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "Size and Placement", EditorStyles.miniButton, GUILayout.Width( 150 ), GUILayout.Height( 20 ) ) )
				ResetSizeAndPlacement( joystick );
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "Style and Options", EditorStyles.miniButton, GUILayout.Width( 150 ), GUILayout.Height( 20 ) ) )
				ResetStyleAndOptions( joystick );
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "Touch Actions", EditorStyles.miniButton, GUILayout.Width( 150 ), GUILayout.Height( 20 ) ) )
				ResetTouchActions( joystick );
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.Space();
		
		// This is for showing helpful tips to help them avoid errors
		if( joystick.joystick == null )
			EditorGUILayout.HelpBox( "Joystick needs to be assigned in 'Assigned Variables'!", MessageType.Error );
		if( joystick.joystickSizeFolder == null )
			EditorGUILayout.HelpBox( "Joystick Size Folder needs to be assigned in 'Assigned Variables'!", MessageType.Error );
		
		if( canvasRect == null )
		{
			if( joystick.transform.root.GetComponent( UnityEngine.UI.CanvasScaler ) )
				canvasRect = joystick.transform.root.GetComponent( UnityEngine.UI.CanvasScaler );
			else
				canvasRect = GetParentCanvas( joystick );
		}
		if( canvasRect != null && canvasRect.uiScaleMode != UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize )
		{
			EditorGUILayout.BeginVertical( "Button" );
			EditorGUILayout.HelpBox( "The Ultimate Joystick cannot be used correctly because the root canvas is using " + canvasRect.uiScaleMode.ToString() + ". Please place the Ultimate Joystick into a different Canvas with the ConstantPixelSize option.", MessageType.Error );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "Adjust Canvas", GUILayout.Width( 100 ), GUILayout.Height( 20 ) ) )
			{
				Debug.Log( "CanvasScaler / ScaleMode option has been adjusted." );
				canvasRect.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
			}
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "Adjust Joystick", GUILayout.Width( 100 ), GUILayout.Height( 20 ) ) )// 75
			{
				var canvasExists : boolean = false;
				var targetCanvas : Transform = joystick.transform;
				var allCanvas : UnityEngine.UI.CanvasScaler[] = GameObject.FindObjectsOfType( UnityEngine.UI.CanvasScaler );
				for( var currCanvas : UnityEngine.UI.CanvasScaler in allCanvas )
				{
					if( canvasExists == false )
					{
						if( currCanvas.uiScaleMode == UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize )
						{
							canvasExists = true;
							targetCanvas = currCanvas.transform;
						}
					}
				}
				// If we didn't find a Canvas with the right options, then we need to make one
				if( canvasExists == false )
				{
					// For full comments, please refer to CreateJoystickEditor.cs
					Debug.Log( "Ultimate UI Canvas has been created." );
					var root : GameObject = new GameObject( "Ultimate UI Canvas" );
					root.layer = LayerMask.NameToLayer( "UI" );
					var canvas : Canvas  = root.AddComponent( Canvas );
					canvas.renderMode = RenderMode.ScreenSpaceOverlay;
					root.AddComponent( UnityEngine.UI.CanvasScaler );
					root.AddComponent( UnityEngine.UI.GraphicRaycaster );
					Undo.RegisterCreatedObjectUndo( root, "Create " + root.name );
					targetCanvas = root.transform;
				}

				// Here we set the joystick to be a child of the canvas
				joystick.transform.SetParent( targetCanvas.transform, false );

				// Focus on the moved Ultimate Joystick gameObject
				Selection.activeGameObject = joystick.gameObject;

				// Tell the user
				Debug.Log( "Ultimate Joystick has been relocated to Ultimate UI Canvas." );
				
				canvasRect = null;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
		}
		
		// This will apply these variables to the selected script
		if( GUI.changed )
			EditorUtility.SetDirty( target );

		// Check and apply colors
		if( ( highlightColor != joystick.highlightColor || highlightColor.a != joystick.highlightColor.a ) && joystick.showHighlight == true )
		{
			highlightColor = joystick.highlightColor;
			joystick.SetHighlight();
		}
		if( tensionColor != joystick.tensionColorNone && joystick.showTension == true )
		{
			tensionColor = joystick.tensionColorNone;
			joystick.SetTensionAccent();
		}
		
		// Check and apply fade
		if( lastFadeOption != joystick.useFade || lastFadeUntouched != joystick.fadeUntouched )
		{
			lastFadeOption = joystick.useFade;
			lastFadeUntouched = joystick.fadeUntouched;

			// If we are using our fade we should set it up in here
			if( joystick.useFade == true )
				joystick.HandleFade( "Untouched" );
			else
				joystick.HandleFade( "Reset" );
		}
	}
	
	function CheckHighlights ( joystick : UltimateJoystickJAVA )
	{
		if( joystick.showHighlight == true )
		{
			// Enable images if they are off
			if( joystick.highlightBase != null && joystick.highlightBase.gameObject.activeInHierarchy == false )
				joystick.highlightBase.gameObject.SetActive( true );
			if( joystick.highlightJoystick != null && joystick.highlightJoystick.gameObject.activeInHierarchy == false )
				joystick.highlightJoystick.gameObject.SetActive( true );
		}
		else
		{
			// Here we want to check if we have any highlights, and if so, SetActive( false )
			if( joystick.highlightBase != null && joystick.highlightBase.gameObject.activeInHierarchy == true )
				joystick.highlightBase.gameObject.SetActive( false );
			if( joystick.highlightJoystick != null && joystick.highlightJoystick.gameObject.activeInHierarchy == true )
				joystick.highlightJoystick.gameObject.SetActive( false );
		}
	}
	
	function CheckTensionAccents ( joystick : UltimateJoystickJAVA )
	{
		if( joystick.showTension == true )
		{
			// Check our images and make sure they are active
			if( joystick.tensionAccentUp != null && joystick.tensionAccentUp.gameObject.activeInHierarchy == false )
				joystick.tensionAccentUp.gameObject.SetActive( true );
			if( joystick.tensionAccentDown != null && joystick.tensionAccentDown.gameObject.activeInHierarchy == false )
				joystick.tensionAccentDown.gameObject.SetActive( true );
			if( joystick.tensionAccentLeft != null && joystick.tensionAccentLeft.gameObject.activeInHierarchy == false )
				joystick.tensionAccentLeft.gameObject.SetActive( true );
			if( joystick.tensionAccentRight != null && joystick.tensionAccentRight.gameObject.activeInHierarchy == false )
				joystick.tensionAccentRight.gameObject.SetActive( true );
			
			// Here we are going to check each tension accent
			if( joystick.tensionAccentUp == null || joystick.tensionAccentDown == null || joystick.tensionAccentLeft == null || joystick.tensionAccentRight == null )
			{
				EditorPrefs.SetBool( "UJ_Variables", true );
				// And send a warning message of which ones are unassigned
				tensionWarnMessage = "Some Tension Accents are unassigned:";
				tensionWarnMessageDefault = tensionWarnMessage;
				if( joystick.tensionAccentUp == null )
				{
					tensionWarnMessage = tensionWarnMessage + " Tension Up";
				}
				if( joystick.tensionAccentDown == null )
				{
					if( tensionWarnMessage != tensionWarnMessageDefault )
						tensionWarnMessage = tensionWarnMessage + ", Tension Down";
					else
						tensionWarnMessage = tensionWarnMessage + " Tension Down";
				}
				if( joystick.tensionAccentLeft == null )
				{
					if( tensionWarnMessage != tensionWarnMessageDefault )
						tensionWarnMessage = tensionWarnMessage + ", Tension Left";
					else
						tensionWarnMessage = tensionWarnMessage + " Tension Left";
				}
				if( joystick.tensionAccentRight == null )
				{
					if( tensionWarnMessage != tensionWarnMessageDefault )
						tensionWarnMessage = tensionWarnMessage + ", Tension Right";
					else
						tensionWarnMessage = tensionWarnMessage + " Tension Right";
				}
				tensionWarnMessage = tensionWarnMessage + ".";
				EditorGUILayout.HelpBox( tensionWarnMessage, MessageType.Warning );
			}
			EditorGUILayout.Space();
		}
		else
		{
			// Here we want to check if we have any tension accents, and if so, SetActive( false )
			if( joystick.tensionAccentUp != null && joystick.tensionAccentUp.gameObject.activeInHierarchy == true )
				joystick.tensionAccentUp.gameObject.SetActive( false );
			if( joystick.tensionAccentDown != null && joystick.tensionAccentDown.gameObject.activeInHierarchy == true )
				joystick.tensionAccentDown.gameObject.SetActive( false );
			if( joystick.tensionAccentLeft != null && joystick.tensionAccentLeft.gameObject.activeInHierarchy == true )
				joystick.tensionAccentLeft.gameObject.SetActive( false );
			if( joystick.tensionAccentRight != null && joystick.tensionAccentRight.gameObject.activeInHierarchy == true )
				joystick.tensionAccentRight.gameObject.SetActive( false );
			// These few lines may show up as red, which would indicate an error, however all the documention supports this and there are no errors while running
		}
	}
	
	function ResetSizeAndPlacement ( joystick : UltimateJoystickJAVA )
	{
		joystick.joystickTouchSize = joystick.JoystickTouchSize.Default;
		joystick.joystickSize = 2.5f;
		joystick.radiusModifier = 4.5f;
		joystick.touchBasedPositioning = false;
		joystick.overrideTouchSize = false;
		joystick.tbp_X = 50.0f;
		joystick.tbp_Y = 75.0f;
		joystick.cs_X = 5.0f;
		joystick.cs_Y = 20.0f;
	}

	function ResetStyleAndOptions ( joystick : UltimateJoystickJAVA )
	{
		joystick.touchPad = false;
		joystick.throwable = true;
		joystick.throwDuration = 0.5f;
		joystick.showHighlight = true;
		joystick.highlightColor = new Color( 0.118f, 0.992f, 0.0f, 1.0f );
		joystick.showTension = true;
		joystick.tensionColorNone = new Color( 0.118f, 0.992f, 0.0f, 0.0f );
		joystick.tensionColorFull = new Color( 0.118f, 0.992f, 0.0f, 1.0f );
		joystick.axis = joystick.Axis.Both;
		joystick.boundary = joystick.Boundary.Circular;
		joystick.tapCountOption = joystick.TapCountOption.NoCount;
	}

	function ResetTouchActions ( joystick : UltimateJoystickJAVA )
	{
		joystick.useAnimation = false;
		joystick.joystickAnimator.enabled = false;
		joystick.useFade = false;
		joystick.fadeUntouched = 1.0f;
		joystick.fadeTouched = 0.5f;
	}
	
	function GetParentCanvas ( joystick : UltimateJoystickJAVA ) : UnityEngine.UI.CanvasScaler
	{
		var parent : Transform = joystick.transform.parent;
		while( parent != null )
		{ 
			if( parent.transform.GetComponent( UnityEngine.UI.CanvasScaler ) )
				return parent.transform.GetComponent( UnityEngine.UI.CanvasScaler );

			parent = parent.transform.parent;
		}
		return null;
	}
}