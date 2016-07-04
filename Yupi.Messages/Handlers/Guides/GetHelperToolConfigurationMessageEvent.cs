﻿using System;

namespace Yupi.Messages.Guides
{
	public class GetHelperToolConfigurationMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			GuideManager guideManager = Yupi.GetGame().GetGuideManager();
			bool onDuty = message.GetBool();

			// TODO Use these values
			message.GetBool();
			message.GetBool();
			message.GetBool();

			if (onDuty)
				guideManager.AddGuide(session);
			else
				guideManager.RemoveGuide(session);

			session.GetHabbo().OnDuty = onDuty;

			router.GetComposer<HelperToolConfigurationMessageComposer> ().Compose (session, onDuty,
				guideManager.GuidesCount, guideManager.HelpersCount, guideManager.GuardiansCount);
		}
	}
}

