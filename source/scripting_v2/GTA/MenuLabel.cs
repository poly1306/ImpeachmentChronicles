
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Drawing;

namespace GTA
{
	[Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class MenuLabel : IMenuItem
	{
		UIText text = null;
		UIRectangle button = null;
		UIRectangle underlineAbove = null;
		UIRectangle underlineBelow = null;
		string caption;

		public MenuLabel(string caption) : this(caption, false)
		{
		}
		public MenuLabel(string caption, bool underlined)
		{
			this.caption = caption;
			Description = string.Empty;
			UnderlineColor = Color.Black;
			UnderlineHeight = 2;
			UnderlinedAbove = false;
			UnderlinedBelow = underlined;
		}

		public virtual void Draw()
		{
			if (button == null || text == null)
			{
				return;
			}

			button.Draw();
			text.Draw();

			if (UnderlinedAbove && underlineAbove != null)
			{
				underlineAbove.Draw();
			}

			if (UnderlinedBelow && underlineBelow != null)
			{
				underlineBelow.Draw();
			}
		}
		public virtual void Draw(Size offset)
		{
			if (button == null || text == null)
			{
				return;
			}

			button.Draw(offset);
			text.Draw(offset);

			if (UnderlinedAbove && underlineAbove != null)
			{
				underlineAbove.Draw(offset);
			}

			if (UnderlinedBelow && underlineBelow != null)
			{
				underlineBelow.Draw(offset);
			}
		}

		public virtual void Select()
		{
			if (button == null)
			{
				return;
			}

			button.Color = Parent.SelectedItemColor;