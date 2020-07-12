The purpose of this script is to help scale and position objects properly and precisely to real world measurements. Also
 since Unity scale is done in Meters, this script allows those of us who are far more used to feet/inches to work easier.

 Useage info:
 
 This replaces the default inspector for transform components. But it trys to replicate it as close as possible.
 
 Unless you select an object with, or with at least one child that has a MeshRenderer/SkinnedMeshRenderer you wont see a
 major difference. No matter if the selected object does or doesnt have one, you will see a arrow next to the Position
 parameter. Clicking it will drop down a list of the objects current world position from 0,0,0, but in different units of
 measurment.
 
 When you do select an object with, or with at least one child that has a MeshRenderer/SkinnedMeshRenderer you will see
 an extra option below position/rotation/scale in the transform, called "Mesh Size". The parameters below list how big the
 select object currently is in that unit of measurment.

 You can chose what units of measurement to show/hide in the "Edit/TransformEx/" menu.
  
 In the Scene view the object you select will have red/green/blue lines drawn around its mesh to help you visualize what
 this script believes is the dimensions of the mesh, and of course the color represents the axis you need to change to
 get what you want. For most objects the measurments you may have for it will work out fine. However, some objects like
 humanoid characters require some thought. A character model may need to be 6"1, but typeing that would include their
 hair as part of that height. Hair tends to add 1/2 - 2 inches. So you may want to use 5"11. Or scale a cube to 6"1 right
 next to it and dial it in manually if you dont want to do the math.
  
 If you want to maintain proportains of an object, you can click the equalize button after adjusting an axis, assuming the
 objects default scale has 1,1,1 proportains across its axis'.
  
  Example:
 
		Say you want a generic cube to be the size of a door, in this case the door being 6 feet 5 inches tall,
                2 1/2 feet wide, and 2 inches thick. You would...

		Set the X value under Feet to 2.5.
		Set the Y value under Feet to 6.5. (More with this in a second...)
		Set the Z value under Inches to 2.

		We acctually set the height to be 6 feet 6 inches above using Feet, but we want it to be 6 feet 5 inches. We
                can use multiple units of measurement to fine tune its size.

		Since we overshot 6"5 by once inch, we can this time use the Y value under inches and simply subtract 1 from
                it. Change 78 to 77 inches.

		And you now have an exactly scaled door!


For script programmers:

	This asset extends Transform with a new extension method.
		
	Transform.TransformEx()

	This has 2 types of methods you can access.

	positionIn****
	sizeIn****

	Replace **** with the unit of measurment you want, like "Vector3 = Transform.TransformEx().positionInInches" and "Transform.TransformEx().sizeInCentimeters = Vector3".

	So if you have a default cube at position 0,0,0, and currently have its Transform in a variable called "cubeTransform", you can access it like this.
	
		Vector3 a = cubeTransform.TransformEx().positionInFeet;
		Debug.Log("The size of this cube in feet is: " + a);
	        // Outputs "The size of this cube in feet is: 3.29, 3.29, 3.29". or something close...

	To set this cube to 100 yards away from 0,0,0 along the Z axis:
	
		cubeTransform.TransformEx().positionInYards = new Vector3(0, 0, 100.0f);



Update Log:

	1.1:		
		The Mesh Size fields are now encased in a foldout like the position fields.
		The RGB boundries lines will now only show in the scene view when the new above mentioned fold out is expanded.
		The open/close status of both fold outs will now be remembered and used between all transforms in the scene.
		Condensed the SizeEx() and PositionEx() extension methods into a single new TransformEx() method.
		Replaced the old GetAs****() and SetFrom****() functions with more proper Get/Set accessors. You now access them like variables.
			I.E.:
				positionIn****
				sizeIn****

	1.2:
		New demo scenes.
		Added menu options to only see the units of measurement that you want.
		Improved the look of the extra Transform controls.
		Ex Position controls added to RectTransforms.
		Added equalize button for easy equalization of the object proportions.
		Fixed a Mesh Size arrow bug where objects were resized when multiple objects were selected.

