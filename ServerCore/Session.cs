﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    internal class Session
    {
        Socket _socket;
        int _disconnected = 0;
        public void Start(Socket socket)
        {
            _socket = socket;
            SocketAsyncEventArgs recvAgs = new SocketAsyncEventArgs();
            recvAgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);            
            recvAgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvAgs);
        }

        public void Send(byte[] sendbuffer)
        {
            _socket.Send(sendbuffer);
        }

        public void Disconnect()
        {
            if(Interlocked.Exchange(ref _disconnected, 1)== 1)
            {
                return;
            }
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }


        #region 네트워크 통신
        void RegisterRecv(SocketAsyncEventArgs args)
        {
            bool pending =_socket.ReceiveAsync(args);
            if (pending == false) 
            {
                OnRecvCompleted(null, args);
            }
        }

        void OnRecvCompleted(object sender, SocketAsyncEventArgs args) 
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success) 
            {
                try
                {
                    string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                    Console.WriteLine($"[From Client] {recvData}");
                    RegisterRecv(args);
                }
                catch (Exception e) 
                {
                    Console.WriteLine($"OnRecvCompleted Failed {e}");
                }                
            }
            else
            {

            }
        }
        #endregion
    }
}
