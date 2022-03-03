//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.IO;
using WinForms = System.Windows.Forms;

namespace GTA
{
	/// <summary>
	/// A base class for all user scripts to inherit.
	/// Only scripts that inherit directly from this class and have a default (parameterless) public constructor will be detected and started.
	/// </summary>
	public abstract class Script
	{
		#region Fields
		ScriptSettings _settings;
		#endregion

		class InstantiateScriptTask : SHVDN.IScriptTask
		{
			internal Type type;
			internal SHVDN.Script script;

			public void Run()
			{
				script = SHVDN.ScriptDomain.CurrentDomain.InstantiateScript(type);
			}
		}

		public Script()
		{
			Name = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this)?.Name ?? string.Empty;
			Filename = SHVDN.ScriptDomain.CurrentDomain.LookupScriptFilename(GetType());
		}

		/// <summary>
		/// An event that is raised every tick of the script.
		/// Put code that needs to be looped each frame in here.
		/// </summary>
		public event EventHandler Tick
		{
			add
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.Tick += value;
				}
			}
			remove
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.Tick -= value;
				}
			}
		}
		/// <summary>
		/// An event that is raised when this <see cref="Script"/> gets aborted for any reason.
		/// This should be used for cleaning up anything created during this <see cref="Script"/>.
		/// </summary>
		public event EventHandler Aborted
		{
			add
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.Aborted += value;
				}
			}
			remove
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.Aborted -= value;
				}
			}
		}

		/// <summary>
		/// An event that is raised when a key is lifted.
		/// The <see cref="System.Windows.Forms.KeyEventArgs"/> contains the key that was lifted.
		/// </summary>
		public event WinForms.KeyEventHandler KeyUp
		{
			add
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.KeyUp += value;
				}
			}
			remove
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.KeyUp -= value;
				}
			}
		}
		/// <summary>
		/// An event that is raised when a key is first pressed.
		/// The <see cref="System.Windows.Forms.KeyEventArgs"/> contains the key that was pressed.
		/// </summary>
		public event WinForms.KeyEventHandler KeyDown
		{
			add
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.KeyDown += value;
				}
			}
			remove
			{
				var script = SHVDN.ScriptDomain.CurrentDomain.LookupScript(this);
				if (script != null)
				{
					script.KeyDown -= value;
				}
			}
		}

		/// <summary>
		/// Gets the name of this <see cref="Script"/>.
		/// </summary>
		public string Name
		{
			get;
		}
		/// <summary>
		/// Gets the filename of this <see cref="Script"/>.
		/// </summary>
		public string Filename
		{
			get;
		}

		/// <summary>
		/// Gets the Directory where this <see cref="Script"/> is stored.
		/// </summary>
		public string BaseDirectory => Path.GetDirectoryName(Filename);

		/// <summary>
		/// Checks if this <see cref="Script"/> is paused.
		/// </summary>
		public bool IsPaused
		{
			get
			{
				return SHVDN.ScriptDomain.CurrentDomain.LookupScript(this).IsPaused;
			}
		}

		/// <summary>
		/// Checks if this <see cref="Script"/> is running.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return SHVDN.ScriptDomain.CurrentDomain.LookupScript(this).IsRunning;
			}
		}

		/// <summary>
		/// Checks if this <see cref="Script"/> is executing.
		/// </summary>
		public bool IsExecuting
		{
			get
			{
				return SHVDN.ScriptDomain.CurrentDomain.LookupScript(this).IsExecuting;
			}
		}

		/// <summary>
		/// Gets an INI file associated with this <see cref="Script"/>.
		/// The File will be in the same location as this <see cref="Script"/> but with an extension of ".ini".
		/// Use th