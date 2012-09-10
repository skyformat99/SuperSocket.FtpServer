using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.SocketBase.Command;
using System.Security.Authentication;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocket.Ftp.FtpService.Command
{
    public class AUTH : FtpCommandBase
    {
        #region StringCommandBase<FtpSession> Members

        public override void ExecuteCommand(FtpSession session, StringRequestInfo requestInfo)
        {
            string ssl = requestInfo.Body;

            switch (ssl)
            {
                case ("SSL"):
                    session.SecureProtocol = SslProtocols.Ssl3;
                    break;
                case ("SSL2"):
                    session.SecureProtocol = SslProtocols.Ssl2;
                    break;
                case ("TLS"):
                    session.SecureProtocol = SslProtocols.Tls;
                    break;
                default:
                    session.SendParameterError();
                    return;
            }

            session.Send(FtpCoreResource.AuthOk_234, ssl);

            session.AppServer.ResetSessionSecurity(session, session.SecureProtocol);
        }

        #endregion
    }
}
