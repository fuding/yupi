﻿using System;

namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class CancellInviteGuide : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			router.GetComposer<OnGuideSessionDetachedMessageComposer> ().Compose (session, 2);

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_GuideFeedbackGiver", 1);
		}
	}
}

