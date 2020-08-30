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
			HeaderColor = Color.FromArgb(200, 255, 20, 147);
			HeaderTextColor = Color.White;
			HeaderTextScale = 0.5f;

			FooterFont = Font.ChaletLondon;
			FooterCentered = false;
			FooterColor = Color.FromArgb(200, 255, 182, 193);
			FooterTextColor = Color.Black;
			FooterTextScale = 0.4f;

			SelectedItemColor = Color.FromArgb(200, 255, 105, 180);
			UnselectedItemColor = Color.FromArgb(200, 176, 196, 222);
			SelectedTextColor = Color.Black;
			UnselectedTextColor = Color.DarkSlateGray;

			ItemFont = Font.ChaletLondon;
			ItemTextScale = 0.4f;
			ItemTextCentered = true;

			Width = 200;
			ItemHeight = 30;
			HeaderHeight = 30;
			FooterHeight = 60;

			HasFooter = true;
		}

		public override void Draw()
		{
			Draw(new Size());
		}
		public override void Draw(Size offset)
		{
			if (rectHeader == null || textHeader == null || (HasFooter && (rectFooter == null || textFooter == null)))
			{
				return;
			}

			if (HasFooter)
			{
				rectFooter.Draw(offset);
				textFooter.Draw(offset);
			}

			rectHeader.Draw(offset);
			textHeader.Draw(offset);

			for (int i = 0; i < ItemDrawCount; i++)
			{
				Items[i + CurrentScrollOffset].Draw(offset);
			}

			DrawScrollArrows(CurrentScrollOffset > 0, CurrentScrollOffset < MaxScrollOffset, offset);
		}

		void DrawScrollArrows(bool up, bool down, Size offset)
		{
			if (!up && !down)
			{
				return;
			}

			if (Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, "CommonMenu"))
			{
				Vector2 Resolution = Function.Call<Vector2>(Hash.GET_TEXTURE_RESOLUTION, "CommonMenu", "arrowright");

				if (up)
				{
					float w = Resolution.X / UI.WIDTH;
					float h = Resolution.Y / UI.HEIGHT;
					float x = (float)(Width + offset.Width) / UI.WIDTH - w * 0.5f;
					float y = (float)(HeaderHeight + offset.Height + ItemHeight / 2) / UI.HEIGHT;

					Function.Call(Hash.DRAW_SPRITE, "CommonMenu", "arrowright", x, y, w, h, -90.0f, 255, 255, 255, 255);
				}
				if (down)
				{
					float w = Resolution.X / UI.WIDTH;
					float h = Resolution.Y / UI.HEIGHT;
					float x = (float)(Width + offset.Width) / UI.WIDTH - w * 0.5f;
					float y = (float)(HeaderHeight + offset.Height + ItemHeight * ItemDrawCount - ItemHeight / 2) / UI.HEIGHT;

			