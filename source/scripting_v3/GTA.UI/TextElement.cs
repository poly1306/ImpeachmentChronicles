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
		/// <param name="position">Set the <see cref="