using GTA;
using GTA.Math;
using GTA.NaturalMotion;
using System;
using System.Collections.Generic;
using System.Linq;

// Mimic electrocution with the stun gun by sending NM messages to the player ped.
// You can find similar config in physicstasks.ymt (search for "CTaskNMShot").
public class FakeElectrocutionShotDemo : Script
{
	public FakeElectrocutionShotDemo()
	{
		KeyDown += FakeElectrocutionShotDemo_KeyDown;
	}

	// Shot_Base (priority: 0)
	static private void StartShotBaseNmMessages(Ped ped)
	{
		var balancerCollisionsReactionNM = ped.Euphoria.BalancerCollisionsReaction;
		balancerCollisionsReactionNM.Start();

		var fallOverWallNM = ped.Euphoria.FallOverWall;
		fallOverWallNM.MoveLegs = true;
		fallOverWallNM.MoveArms = false;
		fallOverWallNM.BendSpine = true;
		fallOverWallNM.RollingPotential = 0.3f;
		fallOverWallNM.RollingBackThr = 0.5f;
		fallOverWallNM.ForceTimeOut = 2f;
		fallOverWallNM.MagOfForce = 0.5f;
		fallOverWallNM.BodyTwist = 0.54f;
		fallOverWallNM.Start();

		var shotNM = ped.Euphoria.Shot;
		shotNM.Start();

		var ShotConfigureArmsNM