﻿using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCodeDumper
{
    public interface IMessageReceiver
    {
        //Task ZeroArgument();
        //Task OneArgument(int x);
        //Task MoreArgument(int x, string y, double z);
        void VoidZeroArgument();
        void VoidOneArgument(int x);
        void VoidMoreArgument(int x, string y, double z);
    }

    public interface ITestHub : IStreamingHub<ITestHub, IMessageReceiver>
    {
        Task ZeroArgument();
        Task OneArgument(int x);
        Task MoreArgument(int x, string y, double z);

        Task<int> RetrunZeroArgument();
        Task<string> RetrunOneArgument(int x);
        Task<double> RetrunMoreArgument(int x, string y, double z);
    }

    public interface ITestService : IService<ITestService>
    {
        UnaryResult<int> Sum(int x, int y);
        Task<UnaryResult<int>> Sum2(int x, int y);


        UnaryResult<MyResponse> Unary1(int x, int y, string z = "unknown");
        UnaryResult<MyResponse> Unary2(MyRequest req);
        UnaryResult<MyResponse> Unary3();
        UnaryResult<Nil> Unary4();
        UnaryResult<MyStructResponse> Unary5(MyStructRequest req);
    }

    public class MyRequest
    {
    }

    public class MyResponse
    {
    }
    public struct MyStructRequest
    {
    }

    public struct MyStructResponse
    {
    }
    public struct Nil
    {
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var _ = DynamicBroadcasterBuilder<IMessageReceiver>.BroadcasterType;
            //var a = MagicOnion.Server.Hubs.AssemblyHolder.Save();
            
            //    var _ = DynamicClientBuilder<ITestService>.ClientType;
            //var c = MagicOnion.Client.DynamicClientAssemblyHolder.Save();
            //Verify(c);

            //var _ = MagicOnionClient.Create<ITestHub>((Channel)null, null);

            //var __ = StreamingHubClientBuilder<ITestHub, IMessageReceiver>.ClientType;
            //var b = MagicOnion.Client.StreamingHubClientAssemblyHolder.Save();

            //Verify(a, b);
        }

        static void Verify(params AssemblyBuilder[] builders)
        {
            var path = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\x64\PEVerify.exe";

            foreach (var targetDll in builders)
            {
                var psi = new ProcessStartInfo(path, targetDll.GetName().Name + ".dll")
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                var p = Process.Start(psi);
                var data = p.StandardOutput.ReadToEnd();
                Console.WriteLine(data);
            }
        }
    }
}
