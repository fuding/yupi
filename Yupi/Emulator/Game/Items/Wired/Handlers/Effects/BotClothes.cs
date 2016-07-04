﻿using System.Collections.Generic;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Items.Wired.Handlers.Effects
{
    public class BotClothes : IWiredItem
    {
        public BotClothes(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            OtherString = string.Empty;
            OtherExtraString = string.Empty;
            OtherExtraString2 = string.Empty;
        }

        public Interaction Type => Interaction.ActionBotClothes;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public string OtherString { get; set; }

        public string OtherExtraString { get; set; }

        public string OtherExtraString2 { get; set; }

        public bool OtherBool { get; set; }

        public bool Execute(params object[] stuff)
        {
            RoomUser bot = Room.GetRoomUserManager().GetBotByName(OtherString);

            if (bot == null || OtherExtraString == "null")
                return false;

            bot.BotData.Look = OtherExtraString;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SetRoomUserMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            bot.Serialize(simpleServerMessageBuffer, false);
            Room.SendMessage(simpleServerMessageBuffer);

            return true;
        }
    }
}