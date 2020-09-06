//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Drawing;

namespace GTA
{
	[Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class MenuEnumScroller : IMenuItem
	{
		UIText text = null;
		UIRectangle button = null;
		int selectedIndex;
		string[] entries;

		public MenuEnumScroller(string caption, string description, string[] entries) : this(caption, description, entries, 0)
		{
		}
		public MenuEnumScroller(string caption, string description, string[] entries, int selectedIndex)
		{
			Caption = caption;
			Description = description;
			this.entries = entries;
			this.selectedIndex = selectedIndex;
		}

		public virtual void Draw()
		{
			Draw(default);
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