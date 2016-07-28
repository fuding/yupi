﻿using System;

namespace Yupi.Messages.User
{
	public class UserGetVolumeSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			UserPreferences preferences = session.UserData.GetHabbo ().Preferences;
			router.GetComposer<LoadVolumeMessageComposer> ().Compose (session, preferences);
		}
	}
}

