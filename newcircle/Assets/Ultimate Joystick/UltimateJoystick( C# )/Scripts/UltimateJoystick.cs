/* Written by Kaz Crowe */
/* UltimateJoystick.cs ver. 1.5.4 */
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
// We are using our own Custom UJImage to avoid overlapping references( Vuforia, ect.. )
using UJImage = UnityEngine.UI.Image;

/* 
 * First off, we are using [ExecuteInEditMode] to be able to show changes in real time.
 * This will not affect anything within a build or play mode. This simply makes the script
 * Able to be run while in the Editor in Edit Mode.
*/
[ExecuteInEditMode]
public class UltimateJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	/* Basic Variables */
	public RectTransform joystick;
	public RectTransform joystickSizeFolder;
	Vector3 joystickCenter;
	Vector2 textureCenter;
	Vector2 defaultPos;
	/* Size and Placement */
	public enum Anchor
	{
		Left,
		Right
	}
	public Anchor anchor;
	public enum JoystickTouchSize
	{
		Default,
		Medium,
		Large
	}
	public JoystickTouchSize joystickTouchSize;
	public float joystickSize = 1.75f;
	public float radiusModifier = 4.5f;
	float radius;
	public bool touchBasedPositioning;
	public bool overrideTouchSize;
	public float tbp_X = 50.0f;
	public float tbp_Y = 75.0f;
	public float cs_X = 5.0f;
	public float cs_Y = 20.0f;
	/* Style and Options */
	public bool touchPad;
	public bool throwable;
	public float throwDuration = 0.5f;
	public bool showHighlight = false;
	public Color highlightColor = new Color( 1, 1, 1, 1 );
	public UJImage highlightBase;
	public UJImage highlightJoystick;
	public bool showTension = false;
	public Color tensionColorNone = new Color( 1, 1, 1, 1 );
	public Color tensionColorFull = new Color( 1, 1, 1, 1 );
	public UJImage tensionAccentUp;
	public UJImage tensionAccentDown;
	public UJImage tensionAccentLeft;
	public UJImage tensionAccentRight;
	public enum Axis
	{
		Both,
		X,
		Y
	}
	public Axis axis;
	public enum Boundary
	{
		Circular,
		Square
	}
	public Boundary boundary;
	/* Touch Actions */
	public enum TapCountOption
	{
		NoCount,
		Accumulate,
		TouchRelease
	}
	public TapCountOption tapCountOption;
	public float tapCountDuration = 0.5f;
	bool countingDown;
	int tapCount = 0;
	public bool useAnimation;
	public Animator joystickAnimator;
	public bool useFade;
	public float fadeUntouched = 1.0f;
	public float fadeTouched = 0.5f;
	public UJImage joystickBase;

	
	void Start ()
	{
		// First off, UpdatePositioning of the joystick
		UpdatePositioning();

		// We need to check our options here, and call the appropriate functions to get them set up
		if( showHighlight == true )
			SetHighlight();
		if( showTension == true )
			SetTensionAccent();
	}
	
	// This function allows other scripts to get our joysticks position data
	public Vector2 JoystickPosition
	{
		get
		{
			// Make a temporary Vector2 and divide it by our radius to give us a value between -1 and 1
			Vector2 tempVec = joystick.position - joystickCenter;
			return tempVec / radius;
		}
	}

	// This function will allow other scripts to get our tapCount if we have it enabled
	public int JoystickTapCount
	{
		get{ return tapCount; }
		set{ tapCount = value; }
	}

	// This means we have touched, so process where we have touched
	public void OnPointerDown ( PointerEventData touchInfo )
	{
		// If we have a throwable joystick, then we want to stop the current moving
		if( throwable == true )
			StopCoroutine( "ThrowableMovement" );

		// If we are using the Touch Based Positioning or TouchPad we need to do a few things here
		if( touchBasedPositioning == true || touchPad == true )
		{
			// We need to move our joysticSizeFolder to the point of our touch
			joystickSizeFolder.position = touchInfo.position - textureCenter;

			// Set our center to the touch position since it has changed
			joystickCenter = touchInfo.position;
		}

		// Call UpdateJoystick with the info from our PointerEventData
		UpdateJoystick( touchInfo );

		// If we want to show animations on Touch, do that here
		if( useAnimation == true )
			joystickAnimator.SetBool( "Touch", true );

		// If we are using our fade on touch do that here as well
		if( useFade == true )
			HandleFade( "Touched" );

		// Check if we are using any TapCountOption
		if( tapCountOption != TapCountOption.NoCount )
		{
			// If we are accumulating taps, then we need to do some things
			if( tapCountOption == TapCountOption.Accumulate )
			{
				// Here, if we aren't counting down, set tapCount to 1 since it's our first touch and start TapCounter
				if( countingDown == false )
				{
					countingDown = true;
					tapCount = 1;
					StartCoroutine( "TapCounter" );
				}
				// If we are NOT currently counting down then increase tapCount
				else
					++tapCount;
			}
			// Otherwise we just set down touches as 1
			else
				tapCount = 1;
		}
	}

	// This means we are moving
	public void OnDrag ( PointerEventData touchInfo )
	{
		// We need to call our UpdateJoystick and process where we are touching
		UpdateJoystick( touchInfo );
	}

	// This means we have let go
	public void OnPointerUp ( PointerEventData touchInfo )
	{
		// If we are using Touch Based Positioning, then we need to change a few things
		if( touchBasedPositioning == true || touchPad == true )
		{
			// we need to set our folder back to defaultPos
			joystickSizeFolder.position = defaultPos;

			// set our joystickCenter again since it changed when we touched down
			joystickCenter = ( Vector2 )joystickSizeFolder.position + textureCenter;
		}

		if( throwable == true )
			StartCoroutine( "ThrowableMovement" );
		else
		{
			// Set the joystick to our center
			joystick.position = joystickCenter;

			// If we have our highlightJoystick then we want to move it back to center
			if( showHighlight == true && highlightJoystick != null )
				highlightJoystick.transform.position = joystickCenter;
		}

		// If we are showing Tension Accents, then we need to reset if we don't have throwable on
		if( showTension == true && throwable == false )
			TensionAccentReset();

		// If we want to show animations on touch up, do that here
		if( useAnimation == true )
			joystickAnimator.SetBool( "Touch", false );

		// If we are using our fade on touch up
		if( useFade == true )
			HandleFade( "Untouched" );

		// Check if we are using TouchAndRelease tapcount and set tapCount to 0 since the touch has released
		if( tapCountOption == TapCountOption.TouchRelease )
			tapCount = 0;
	}

	// This function updates our joystick according to our touch
	void UpdateJoystick ( PointerEventData touchInfo )
	{
		// Create a new Vector2 to equal the vector from our curret touch to the center of joystick
		Vector2 tempVector = touchInfo.position - ( Vector2 )joystickCenter;

		// If we are using just X or Y, then we just need to zero out the right value
		if( axis == Axis.X )
			tempVector.y = 0;
		else if( axis == Axis.Y )
			tempVector.x = 0;

		if( boundary == Boundary.Circular )
		{
			// Clamp the Vector, which will give us a round boundary
			tempVector = Vector2.ClampMagnitude( tempVector, radius );
		}
		else if( boundary == Boundary.Square )
		{
			// We want to Clamp both X and Y seperately so we get a square boundary
			tempVector.x = Mathf.Clamp( tempVector.x,  -radius,  radius );
			tempVector.y = Mathf.Clamp( tempVector.y,  -radius,  radius );
		}

		// Apply the Vector to our Joystick's position
		joystick.transform.position = ( Vector2 )joystickCenter + tempVector;

		// If we have our highlightJoystick then we want to move to the same place as our joystick
		if( highlightJoystick != null && showHighlight == true )
			highlightJoystick.transform.position = joystick.transform.position;

		// If we are using the style Ultimate Joystick and if showTension is true, display Tension
		if( showTension == true )
			TensionAccentDisplay();
	}

	// This function updates our options. It is public so we can call it from other scripts to update our positioning
	public void UpdatePositioning ()
	{
		// We want our joystick size to be larger when we have a larger number, so we need to calculate that out
		float textureSize = Screen.height * ( joystickSize / 10 );

		// Same with our radius modifier
		radius = textureSize * ( radiusModifier / 10 );

		// We need to store this object's RectTrans so that we can position it
		RectTransform baseTrans = GetComponent<RectTransform>();

		// We need to get a position for our joystick based on our position variable
		Vector2 joystickTexturePosition = ConfigureTexturePosition( textureSize );

		// If we are wanting our touch size to override our JoystickTouchSize option
		if( overrideTouchSize == true )
		{
			// Fix our size variables
			float fixedFBPX = tbp_X / 100;
			float fixedFBPY = tbp_Y / 100;

			// Depending on our joystickTouchSize options, we need to configure our size
			baseTrans.sizeDelta = new Vector2( Screen.width * fixedFBPX, Screen.height * fixedFBPY );

			// If anchor is set to Left, then we want to set it in the bottom left corner
			if( anchor == Anchor.Left )
				baseTrans.position = new Vector2( 0, 0 );
			else // it is right, then we want it to be in the center of our screen
			{
				if( overrideTouchSize == true )
					baseTrans.position = new Vector2( Screen.width - baseTrans.sizeDelta.x, 0 );
				else
					baseTrans.position = new Vector2( Screen.width / 2, 0 );
			}
		}
		else
		{
			// Our touch size needs to be fixed to a float value
			float fixedTouchSize;
			if( joystickTouchSize == JoystickTouchSize.Large )
				fixedTouchSize = 2.0f;
			else if( joystickTouchSize == JoystickTouchSize.Medium )
				fixedTouchSize = 1.51f;
			else
				fixedTouchSize = 1.01f;

			// Make a temporary Vector2
			Vector2 tempVector = new Vector2( textureSize, textureSize );

			// Our touch area is standard, so set it up with our tempVector multiplied by our fixedTouchSize
			baseTrans.sizeDelta = tempVector * fixedTouchSize;

			// We get our texture position and modify it with our sizeDelta - tempVector ( gives us the difference ) and divide by 2
			baseTrans.position = joystickTexturePosition - ( ( baseTrans.sizeDelta - tempVector ) / 2 );
		}

		if( touchBasedPositioning == true || touchPad == true )
		{
			// We need to know our texture's center so that we can move it to our touch position correctly
			textureCenter = new Vector2( textureSize / 2, textureSize / 2 );
			
			// Also need to store our default position so that we can return after the touch has been lifted
			defaultPos = joystickTexturePosition;
		}

		// Our joystickSizeFolder needs to be textureSize and texture position
		joystickSizeFolder.sizeDelta = new Vector2( textureSize, textureSize );
		joystickSizeFolder.position = joystickTexturePosition;

		// Store the joystick center so we can return to it
		joystickCenter = joystick.position;
	}

	// This function will configure a Vector2 for the position of our Joystick
	Vector2 ConfigureTexturePosition ( float textureSize )
	{
		// We need a few temporary Vector2's to work with
		Vector2 tempPosVector;

		// we need to fix our custom spacing variables to be usable
		float fixedCSX = cs_X / 100;
		float fixedCSY = cs_Y / 100;

		// We also need two floats for applying our spacers according to our screen size
		float positionSpacerX = Screen.width * fixedCSX - ( textureSize * fixedCSX );
		float positionSpacerY = Screen.height * fixedCSY - ( textureSize * fixedCSY );

		// If it's left, we can simply apply our positionxSpacerX
		if( anchor == Anchor.Left )
			tempPosVector.x = positionSpacerX;
		// else it's right, we need to calculate out from our right side and apply our positionSpaceX
		else
			tempPosVector.x = ( Screen.width - textureSize ) - positionSpacerX;

		// Here we just apply our positionSpacerY
		tempPosVector.y = positionSpacerY;

		// Return our updated Vector2
		return tempPosVector;
	}

	// This function is called to set up our Highlight Images
	public void SetHighlight ()
	{
		// Here we need to check if each variable is assigned so we don't get a null reference log error when applying color
		if( highlightBase != null )
			highlightBase.color = highlightColor;
		if( highlightJoystick != null )
			highlightJoystick.color = highlightColor;

		// If we are using fade, then we want to modify our highlight's color
		if( useFade == true )
			HandleFade( "Untouched" );
	}

	/* These next functions are for our Tension Accents */
	public void SetTensionAccent ()
	{
		// We need to check if ANY of our tension accents are unassigned
		if( tensionAccentUp == null || tensionAccentDown == null || tensionAccentLeft == null || tensionAccentRight == null )
		{
			// Disable showTension to avoid errors, and let the user know with a Debug
			Debug.LogError( "Not all Tension Accent's are assign. Disabling Show Tension to avoid errors. Please assign all Tension Accents before modifying color values." );
			showTension = false;
		}
		// else we have all variables assigned and we can use them without errors
		else
			TensionAccentReset();
	}

	// This function is called when our joystick is moving
	void TensionAccentDisplay ()
	{
		// We need a Vector2 to store our JoystickPosition
		Vector2 tension = JoystickPosition;

		// If our joystick is to the right
		if( tension.x > 0 )
		{
			// Then we lerp our color according to our X position
			tensionAccentRight.color = Color.Lerp( tensionColorNone, tensionColorFull, tension.x );
			
			// If our tensionAccentLeft is not tensionColorNone, we want to make it so
			if( tensionAccentLeft.color != tensionColorNone )
				tensionAccentLeft.color = tensionColorNone;
		}
		// else our joystick is to the left
		else
		{
			// Mathf.Abs gives us a positive number to work with
			tension.x = Mathf.Abs( tension.x );
			tensionAccentLeft.color = Color.Lerp( tensionColorNone, tensionColorFull, tension.x );

			if( tensionAccentRight.color != tensionColorNone )
				tensionAccentRight.color = tensionColorNone;
		}

		// If our joystick is up
		if( tension.y > 0 )
		{
			// Then we lerp our color according to our Y position
			tensionAccentUp.color = Color.Lerp( tensionColorNone, tensionColorFull, tension.y );

			// If our tensionAccentDown is not tensionColorNone, we want to make it so
			if( tensionAccentDown.color != tensionColorNone )
				tensionAccentDown.color = tensionColorNone;
		}
		// else it is down
		else
		{
			// Mathf.Abs gives us a positive number to work with
			tension.y = Mathf.Abs( tension.y );
			tensionAccentDown.color = Color.Lerp( tensionColorNone, tensionColorFull, tension.y );

			if( tensionAccentUp.color != tensionColorNone )
				tensionAccentUp.color = tensionColorNone;
		}
	}
	
	void TensionAccentReset ()
	{
		// Reset our tension colors back to tensionColorNone
		tensionAccentUp.color = tensionColorNone;
		tensionAccentDown.color = tensionColorNone;
		tensionAccentLeft.color = tensionColorNone;
		tensionAccentRight.color = tensionColorNone;
	}

	public void HandleFade ( string fadeAction )
	{
		// Temporary float to hold our modifier for our color.a
		float alphaMod;

		// Based on our fadeAction, we will modify our alphaMod for use
		if( fadeAction == "Touched" )
			alphaMod = fadeTouched;
		else if( fadeAction == "Untouched" )
			alphaMod = fadeUntouched;
		else
			alphaMod = 1.0f;

		// We need to check if both these are assigned
		if( joystickBase != null && joystick != null )
		{
			// And get a temporary color that is the same as our joystickBase, then change the alpha to our alphaMod
			Color joystickColor = joystickBase.color;
			joystickColor.a = alphaMod;

			// Now apply the temporary color to our joystickBase and joystick
			joystickBase.color = joystickColor;
			joystick.GetComponent<UJImage>().color = joystickColor;
		}

		// Check if we are truely using our highlights
		if( showHighlight == true )
		{
			// We want our fade to scale correctly with our current highlightColor.a
			float hlAlphaMod = highlightColor.a * alphaMod;

			// Check our highlightBase Image
			if( highlightBase != null )
			{
				// Temporary Color variable
				Color highlightBaseColor = highlightBase.color;

				// If we are using Fade, then we want to change to our alphaMod
				if( useFade == true )
					highlightBaseColor.a = hlAlphaMod;
				// However, if we are not, then we want our HL to be the same as our highlightColor option
				else
					highlightBaseColor.a = highlightColor.a;

				// Apply our new color to our highlightBase
				highlightBase.color = highlightBaseColor;
			}

			// Repeat the steps we just did for our highlightBase
			if( highlightJoystick != null )
			{
				Color highlightJoyColor = highlightJoystick.color;
				if( useFade == true )
					highlightJoyColor.a = hlAlphaMod;
				else
					highlightJoyColor.a = highlightColor.a;

				highlightJoystick.color = highlightJoyColor;
			}
		}
	}

	// This function delays for our tapCountDuration to accumulate taps
	IEnumerator TapCounter ()
	{
		// wait for our tapCountDuration
		yield return new WaitForSeconds( tapCountDuration );

		// Then reset our tapCount and countingDown
		tapCount = 0;
		countingDown = false;
	}

	// Function for moving our joystickk kback to center over a set time
	IEnumerator ThrowableMovement ()
	{
		// Fix our throwDuration into a speed
		float throwSpeed = 1.0f / throwDuration;

		// Store the position of where the joystick is currently
		Vector3 startJoyPos = joystick.position;

		// Boolean to control our slow at the end of the throw
		bool hasSlowed = false;
		for( float i = 0.0f; i < 1.0f; i += Time.deltaTime * throwSpeed )
		{
			// Lerp our joystick's position from where this coroutine started to our center
			joystick.position = Vector3.Lerp( startJoyPos, joystickCenter, i );
			if( showHighlight == true && highlightJoystick != null )
				highlightJoystick.transform.position = joystick.position;

			// This will slow our joystick down at the end
			if( i >= 0.85f && hasSlowed == false )
			{
				hasSlowed = true;
				throwSpeed *= 0.5f;
			}

			// If we are showing tension, then we want to display that as it moves
			if( showTension == true )
				TensionAccentDisplay();

			yield return null;
		}
		// Finalize our joystick's position
		joystick.position = joystickCenter;
		if( showHighlight == true && highlightJoystick != null )
			highlightJoystick.transform.position = joystick.position;

		// Here at the end, we want to just reset our tension
		if( showTension == true )
			TensionAccentReset();
	}

	/* 
	 * This function allows us to apply changes in screen size and joystick options in real time
	 * However anything within this #if section will not be run in a game build, only within Unity
	*/
#if UNITY_EDITOR
	void Update ()
	{
		// We want to constantly keep our joystick updated while the game is not being run in Unity
		if( Application.isPlaying == false )
		{
			RectTransform baseTrans = GetComponent<RectTransform>();
			if( baseTrans.transform.localScale.x != 1 )
				baseTrans.transform.localScale = new Vector3( 1, 1, 1 );
				
			UpdatePositioning();
		}
	}
#endif
}