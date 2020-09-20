//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Drawing;

namespace GTA
{
	[Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class MenuToggle : IMenuItem
	{
		UIText text = null;
		UIRectangle button = null;
		bool toggleSelection;

		public MenuToggle(string caption, string description) : this(caption, description, false)
		{
		}
		public MenuToggle(string caption, string description, bool value)
		{
			Caption = caption;
			Description = description;
			toggleSelection = value;
		}

		public virtual void Draw()
		{
			Draw(default);
		}
		public virtual void Draw(Size offset)
		{
			