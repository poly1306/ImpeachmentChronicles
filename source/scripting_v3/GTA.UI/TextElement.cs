//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace GTA.UI
{
	public class TextElement : IElement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		public TextElement(string caption, PointF position, float scale) :
			this(caption, position, scale, Color.WhiteSmoke, Font.ChaletLondon, Alignment.Left, false, false, 0.0f)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		/// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="TextElement"/>.</param>
		public TextElement(string caption, PointF position, float scale, Color color) :
			this(caption, position, scale, color, Font.ChaletLondon, Alignment.Left, false, false, 0.0f)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		/// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="TextElement"/>.</param>
		/// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
		public TextElement(string caption, PointF position, float scale, Color color, Font font) :
			this(caption, position, scale, color, font, Alignment.Left, false, false, 0.0f)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		/// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="TextElement"/>.</param>
		/// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
		/// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="GTA.UI.Alignment.Left"/>,<see cref="GTA.UI.Alignment.Center"/> or <see cref="GTA.UI.Alignment.Right"/>.</param>
		public TextElement(string caption, PointF position, float scale, Color color, Font font, Alignment alignment) :
			this(caption, position, scale, color, font, alignment, false, false, 0.0f)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		/// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="TextElement"/>.</param>
		/// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
		/// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="GTA.UI.Alignment.Left"/>,<see cref="GTA.UI.Alignment.Center"/> or <see cref="GTA.UI.Alignment.Right"/>.</param>
		/// <param name="shadow">Sets whether or not to draw the <see cref="TextElement"/> with a <see cref="Shadow"/> effect.</param>
		/// <param name="outline">Sets whether or not to draw the <see cref="TextElement"/> with an <see cref="Outline"/> around the letters.</param>
		public TextElement(string caption, PointF position, float scale, Color color, Font font, Alignment alignment, bool shadow, bool outline) :
			this(caption, position, scale, color, font, alignment, shadow, outline, 0.0f)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextElement"/> class used for drawing text on the screen.
		/// </summary>
		/// <param name="caption">The <see cref="TextElement"/> to draw.</param>
		/// <param name="position">Set the <see cref="Position"/> on screen where to draw the <see cref="TextElement"/>.</param>
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decre