using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.SocketBase.Command;
using Raccent.Ftp.FtpService.Storage;
using SuperSocket.SocketBase.Protocol;

namespace Raccent.Ftp.FtpService.Command
{
    public class RNFR : StringCommandBase<FtpSession>
    {
        #region StringCommandBase<FtpSession> Members

        public override void ExecuteCommand(FtpSession session, StringRequestInfo requestInfo)
        {
            if (!session.Logged)
                return;

            string filepath = requestInfo.Data;

            if (string.IsNullOrEmpty(filepath))
            {
                session.SendParameterError();
                return;
            }

            long folderID = 0;

            if (session.AppServer.FtpServiceProvider.IsExistFile(session.Context, filepath))
            {
                session.Context.RenameFor = filepath;
                session.Context.RenameItemType = ItemType.File;
                session.SendResponse(Resource.RenameForOk_350);
            }
            else if (session.AppServer.FtpServiceProvider.IsExistFolder(session.Context, filepath, out folderID))
            {
                session.Context.RenameFor = filepath;
                session.Context.RenameItemType = ItemType.Folder;
                session.SendResponse(Resource.RenameForOk_350);
            }
            else
            {
                session.SendResponse(session.Context.Message);
            }
        }

        #endregion
    }
}