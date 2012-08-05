using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace Raccent.Ftp.FtpService.Command
{
    public class STOU : STOR
    {
        public override void ExecuteCommand(FtpSession session, StringRequestInfo requestInfo)
        {
            string param = requestInfo.Data;
            if (string.IsNullOrEmpty(requestInfo.Data))
                param = Guid.NewGuid().ToString();

            base.ExecuteCommand(session, new StringRequestInfo(requestInfo.Key, param, new string[]{ }));
        }
    }
}