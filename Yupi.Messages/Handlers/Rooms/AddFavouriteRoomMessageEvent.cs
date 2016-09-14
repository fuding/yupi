﻿using System;
using Yupi.Messages.Rooms;


namespace Yupi.Messages.Rooms
{
    public class AddFavouriteRoomMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();
            router.GetComposer<FavouriteRoomsUpdateMessageComposer>().Compose(session, roomId, true);
            throw new NotImplementedException();
            //session.Info.FavoriteRooms.Add(roomId);
        }
    }
}