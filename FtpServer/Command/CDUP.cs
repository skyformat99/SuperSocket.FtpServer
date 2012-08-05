using System;
using System.Collections.Generic;
using System.Text;
using Raccent.Ftp.FtpCommon;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace Raccent.Ftp.FtpService.Command
{
    public class CDUP : StringCommandBase<FtpSession>
    {
        #region StringCommandBase<FtpSession> Members

        public override void ExecuteCommand(FtpSession session, StringRequestInfo requestInfo)
        {
            if (!session.Logged)
                return;

            if (string.IsNullOrEmpty(session.Context.CurrentPath) || session.Context.CurrentPath == "/")
            {
                session.SendResponse(Resource.NotFound_550);
                return;
            }

            string path = StringUtil.GetParentDirectory(session.Context.CurrentPath, '/');

            long folderID;

            if (session.AppServer.FtpServiceProvider.IsExistFolder(session.Context, path, out folderID))
            {
                session.Context.CurrentPath = path;

                if (folderID > 0)
                    session.Context.CurrentFolderID = folderID;

                session.SendResponse(string.Format(Resource.ChangeDirectoryUp_250, path));
            }
            else
            {
                if (session.Context.Status == FtpStatus.Error)
                    session.SendResponse(session.Context.Message);
                else
                    session.SendResponse(Resource.NotFound_550);
            }
        }

        #endregion
    }
}