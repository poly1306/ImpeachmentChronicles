//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Drawing;

namespace GTA
{
	[Obsolete("The built-in menu implementation is obsolete. Please consider using external alternatives instead.")]
	public class MessageBox : MenuBase
	{
		UIRectangle rectNo = null;
		UIRectangle rectYes = null;
		UIRectangle rectBody = null;
		UIText text = null;
		UIText textNo = null;
		UIText textYes = null;
		bool selection = true;

		public MessageBox(string headerCaption)
		{
			HeaderTextColor = C