﻿using System;
using Yupi.Controller;



namespace Yupi.Messages.Navigator
{
	public class GoToHotelViewMessageEvent : AbstractHandler
	{
		private RoomManager RoomManager;

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.Room == null)
				return;

			RoomManager.RemoveUser (session.RoomEntity);

			// TODO Implement
			/*
			HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

			if (hotelView.FurniRewardName != null)
			{
				router.GetComposer<LandingRewardMessageComposer> ().Compose (session, hotelView);
			}*/

			session.Room = null;
		}
	}
}

