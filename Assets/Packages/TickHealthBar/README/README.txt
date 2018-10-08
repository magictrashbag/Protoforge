The "HealthBarComponent" is where you can manipulate the health bar either through the inspector or through code.

It should be attached to a Game Object that also has UI/Image component attached, and the Image component
should have a "HealthBarMaterial" selected. The Image component should NOT have a sprite assigned, otherwise the Health Bar shader will do strange things. 
The "HealthBarExamples" scene found in the "Examples" folder shows a variety of health bars set up in this manner, and can be used for your understanding of how to set everything up.

The "HealthBarComponent" automatically gets the image dimensions and passes them to the Health Bar Shader, so that things like the border and tick lines can be drawn consistently
regardless of the image width and height.

When referencing the component inside of your own scripts, make sure to use the 'Fizzik' namespace.

// -----------------------------------------------------------------------------
// Copyright (c) 2018 Maxim Tiourin
// Please direct any bug reports/feedback to maximtiourin@gmail.com