//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GTA
{
	// [Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class Menu : MenuBase
	{
		UIRectangle rectHeader = null;
		UIRectangle rectFooter = null;
		UIText textHeader = null;
		UIText textFooter = null;
		int selectedIndex = -1;
		int maxDrawLimit = 10;
		int startScrollOffset = 2;
		int scrollOffset = 0;
		string footerDescription = "footer description";

		public Menu(string headerCaption, IMenuItem[] items) : this(headerCaption, items, 10)
		{
		}
		public Menu(string headerCaption, IMenuItem[] items, int MaxItemsToDraw)
		{
			// Put the items in the item stack
			// The menu itself will be initialized when it gets added to the viewport
			foreach (IMenuItem item in items)
			{
				Items.Add(item);
				item.Parent = this;
			}

			Caption = headerCaption;

			MaxDrawLimit = MaxItemsToDraw;

			// Set defaults for the properties
			HeaderFont = Font.HouseScript;
			HeaderCentered = true;
			HeaderColor = Color.FromArgb(200, 255