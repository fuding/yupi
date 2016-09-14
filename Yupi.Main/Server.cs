﻿using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Net;
using Yupi.Controller;
using Yupi.Protocol.Buffers;
using Yupi.Messages;
using Yupi.Messages.User;
using Yupi.Messages.Achievements;
using log4net;
using Yupi.Util;
using log4net.Appender;
using Yupi.Model.Repository;
using log4net.Repository.Hierarchy;
using Yupi.Rest;

namespace Yupi.Main
{
    public class Server
    {
        private IServer<Habbo> TCPServer;
        private ClientManager ClientManager;
        private RestServer RestServer;

        public Server()
        {
            SetupLogger();

            // TODO Close Session & Multiple sessions!
            var factory = ModelHelper.CreateFactory();
            var dbSession = factory.OpenSession();

            DependencyFactory.RegisterInstance(dbSession);

            // TODO Don't run this if DB is not new!
            ModelHelper.Populate();
            Router.Default = new Router("PRODUCTION-201510201205-42435347", "../../../Config/",
                typeof(AchievementProgressMessageComposer).Assembly);

            ClientManager = DependencyFactory.Resolve<ClientManager>();

            RestServer = new RestServer();

            SetupTCP();
        }

        private void SetupTCP()
        {
            TCPServer = ServerFactory<Habbo>.CreateServer(30000);

            TCPServer.OnConnectionOpened += ClientManager.AddClient; // TODO Connection security!
            TCPServer.OnConnectionClosed += ClientManager.RemoveClient;
            TCPServer.OnMessageReceived += (ISession<Habbo> session, byte[] body) =>
            {
                //using(global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = ClientMessageFactory.GetClientMessage()) {
                // TODO When using message pool the SimpleClientMessageBuffer becomes invalid (after several messages) -> DEBUG
                ClientMessage message = new ClientMessage();
                message.Setup(body);
                Router.Default.Handle(session.UserData, message);
            };
        }

        private void SetupLogger()
        {
            Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();
            hierarchy.Root.Level = log4net.Core.Level.Debug;
            hierarchy.RaiseConfigurationChanged(EventArgs.Empty);

            AppenderSkeleton appender;
            if (MonoUtil.IsRunningOnMono())
            {
                appender = new log4net.Appender.ConsoleAppender();
            }
            else
            {
                appender = new log4net.Appender.ColoredConsoleAppender();
            }
            // TODO Add File Appender
            appender.Layout = new log4net.Layout.PatternLayout(@"%date [%c{2}] %-5level %message%newline");
            appender.Threshold = log4net.Core.Level.Debug;
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);
            /*
            var fileAppender = new log4net.Appender.FileAppender ();
            fileAppender.AppendToFile = true;
            fileAppender.Layout = new log4net.Layout.PatternLayout(@"%date %-5level %message%newline");
            fileAppender.Threshold = log4net.Core.Level.Debug;
            fileAppender.File = "log.txt";
            fileAppender.ActivateOptions ();
            log4net.Config.BasicConfigurator.Configure(fileAppender);*/
        }

        public void Run()
        {
            TCPServer.Start();
            RestServer.Start();
        }
    }
}