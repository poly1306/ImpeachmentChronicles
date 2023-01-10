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
		/// <param name="scale">Sets a <see cref="Scale"/> used to increase of decrease the size of the <see cref="TextElement"/>, for no scaling use 1.0f.</param>
		/// <param name="color">Set the <see cref="Color"/> used to draw the <see cref="TextElement"/>.</param>
		/// <param name="font">Sets the <see cref="Font"/> used when drawing the text.</param>
		/// <param name="alignment">Sets the <see cref="Alignment"/> used when drawing the text, <see cref="GTA.UI.Alignment.Left"/>,<see cref="GTA.UI.Alignment.Center"/> or <see cref="GTA.UI.Alignment.Right"/>.</param>
		/// <param name="shadow">Sets whether or not to draw the <see cref="TextElement"/> with a <see cref="Shadow"/> effect.</param>
		/// <param name="outline">Sets whether or not to draw the <see cref="TextElement"/> with an <see cref="Outline"/> around the letters.</param>
		/// <param name="wrapWidth">Sets how many horizontal pixel to draw before wrapping the <see cref="TextElement"/> on the next line down.</param>
		public TextElement(string caption, PointF position, float scale, Color color, Font font, Alignment alignment, bool shadow, bool outline, float wrapWidth)
		{
			_pinnedText = new List<IntPtr>();
			Enabled = true;
			Caption = caption;
			Position = position;
			Scale = scale;
			Color = color;
			Font = font;
			Alignment = alignment;
			Shadow = shadow;
			Outline = outline;
			WrapWidth = wrapWidth;
		}

		~TextElement()
		{
			foreach (var ptr in _pinnedText)
			{
				Marshal.FreeCoTaskMem(ptr); //free any existing allocated text
			}
			_pinnedText.Clear();
		}

		private string _caption;
		private readonly List<IntPtr> _pinnedText;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TextElement" /> will be drawn.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.
		/// </value>
		public bool Enabled
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the color of this <see cref="TextElement" />.
		/// </summary>
		/// <value>
		/// The color.
		/// </value>
		public Color Color
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the position of this <see cref="TextElement" />.
		/// </summary>
		/// <value>
		/// The position scaled on a 1280*720 pixel base.
		/// </value>
		/// <remarks>
		/// If ScaledDraw is called, the position will be scaled by the width returned in <see cref="Screen.ScaledWidth" />.
		/// </remarks>
		public PointF Position
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the scale of this <see cref="TextElement"/>.
		/// </summary>
		/// <value>
		/// The scale usually a value between ~0.5 and 3.0, Default = 1.0
		/// </value>
		public float Scale
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the font of this <see cref="TextElement"/>.
		/// </summary>
		/// <value>
		/// The GTA Font use when drawing.
		/// </value>
		public Font Font
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the text to draw in this <see cref="TextElement"/>.
		/// </summary>
		/// <value>
		/// The caption.
		/// </value>
		public string Caption
		{
			get
			{
				return _caption;
			}
			set
			{
				_caption = value;
				foreach (var ptr in _pinnedText)
				{
					Marshal.FreeCoTaskMem(ptr); //free any existing allocated text
				}
				_pinnedText.Clear();

				SHVDN.NativeFunc.PushLongString(value, (string str) =>
				{
					byte[] data = Encoding.UTF8.GetBytes(str + "\0");
					IntPtr next = Marshal.AllocCoTaskMem(data.Length);
					Marshal.Copy(data, 0, next, data.Length);
					_pinnedText.Add(next);
				});
			}
		}

		/// <summary>
		/// Gets or sets the alignment of this <see cref="TextElement"/>.
		/// </summary>
		/// <value>
		/// The alignment:<c>Left</c>, <c>Center</c>, <c>Right</c> Justify
		/// </value>
		public Alignment Alignment
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TextElement"/> is drawn with a shadow effect.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if shadow; otherwise, <see langword="false" />.
		/// </value>
		public bool Shadow
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TextElement"/> is drawn with an outline.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if outline; otherwise, <see langword="false" />.
		/// </value>
		public bool Outline
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets the maximum size of the <see cref="TextElement"/> before it wraps to a new line.
		/// </summary>
		/// <value>
		/// The width of the <see cref="TextElement"/>.
		/// </value>
		public float WrapWidth
		{
			get; set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether the alignment of this <see cref="TextElement" /> is centered.
		/// See <see cref="Alignment"/>
		/// </summary>
		/// <value>
		///   <see langword="true" /> if centered; otherwise, <see langword="false" />.
		/// </value>
		public bool Centered
		{
			get
			{
				return Alignment == Alignment.Center;
			}
			set
			{
				if (value)
				{
					Alignment = Alignment.Center;
				}
			}
		}

		/// <summary>
		/// Measures how many pixels in the horizontal axis this <see cref="TextElement"/> will use when drawn	against a 1280 pixel base
		/// </summary>
		public float Width
		{
			get
			{
				Function.Call(Hash.BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT, SHVDN.NativeMemory.CellEmailBcon);

				foreach (IntPtr ptr in _pinnedText)
				{
					Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, ptr);
				}

				Function.Call(Hash.SET_TEXT_FONT, Font);
				Function.Call(Hash.SET_TEXT_SCALE, Scale, Scale);

				return Screen.Width * Function.Call<float>(Hash.END_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT, 1);
			}
		}
		/// <summary>
		/// Measures how many pixels in the horizontal axis this <see cref="TextElement"/> will use when drawn against a <see cref="ScaledWidth"/> pixel base
		/// </summary>
		public float ScaledWidth
		{
			get
			{
				Function.Call(Hash.BEGIN_TEXT_COMMAND_GET_SCREEN_WIDTH_OF_DISPLAY_TEXT, SHVDN.NativeMemory.CellEmailBcon);

				foreach (IntPtr ptr in _pinnedText)
				{
					Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, ptr);
				}

				Function.Call(Hash.SET_TEXT_FONT, Font);
				Function.Call(Hash.SET_TEXT_SCALE, Scale, Scale);

				return Screen.ScaledWidth * Function.Call<float>(Hash.END_TEXT_COMMAND_GET_SC