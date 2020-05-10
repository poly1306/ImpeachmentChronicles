using GTA;

public class EuphoriaDemo : Script
{
	public EuphoriaDemo()
	{
		KeyDown += EuphoriaDemo_KeyDown;
	}

	private void EuphoriaDemo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
	{
		Ped playerPed = Game.Player.Character;

		if (e.KeyCode == System.Windows.Forms.Keys.J)
		{
			// Make sure all arguments have their default value
			playerPed.Euphoria.ArmsWindmillAdaptive.ResetArguments();

			// Set all the arguments used for the message
			// IntelliSense shows a list of available arguments
			playerPed.Euphoria.ArmsWindmillAdaptive.AngSpeed = 10f;
			playerPed.Euphoria.ArmsWindmillAdaptive.BodyStiffness = 0f;
			playerPed.Euphoria.ArmsWindmillAdaptive.Amplitude = 2f;
			playerPed.Euphoria.ArmsWindmillAdaptive.LeftElbowAngle = 6f;
			playerPed.Euphoria.ArmsWindmillAdaptive.RightElbowAngle = 6f;
			playerPed.Euphoria.ArmsWindmillAdaptive.DisableOnImpact = false;
			playerPed.Euphoria.ArmsWindmillAdaptive.BendLeftElbow = true;
			playerPed.Eupho