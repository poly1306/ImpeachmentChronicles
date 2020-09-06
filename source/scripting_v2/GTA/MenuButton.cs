//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Drawing;

namespace GTA
{
	[Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class MenuButton : IMenuItem
	{
		UIText text = null;
		UIRectangle button = null;
		string caption;

		public MenuButton(string caption) : this(caption, string.Empty)
		{
		}
		public MenuButton(string caption, string description)
		{
			this.caption = caption;
			Description = description;
		}

		public virtual void Draw()
		{
			if (button == null || text == null)
			{
				return;
			}

			button.Draw();
			text.Draw();
		}
		public virtual void Draw(Size offset)
		{
			if (button == null || text == null)
			{
				return;
			}

			button.Draw(offset);
			text.Draw(offset);
		}

		public virtual void Select()
		{
			if (button == null)
			{
				return;
			}

			button.Color = Parent.SelectedItemColor;
			text.Color = Parent.SelectedTextColor;
		