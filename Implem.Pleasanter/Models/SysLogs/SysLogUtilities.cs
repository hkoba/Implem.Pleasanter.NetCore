﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Classes;
using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.Extensions;
using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.HtmlParts;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Resources;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Implem.Pleasanter.Models
{
    public static class SysLogUtilities
    {
        /// <summary>
        /// Fixed:
        /// </summary>
        public static void Maintain(IContext context)
        {
            if (Parameters.SysLog.RetentionPeriod > 0 &&
                (DateTime.Now - Applications.SysLogsMaintenanceDate).Days > 0)
            { 
                Rds.ExecuteNonQuery(
                    context: context,
                    statements: Rds.PhysicalDeleteSysLogs(
                        where: Rds.SysLogsWhere().CreatedTime(
                            DateTime.Now.Date.AddDays(
                                Parameters.SysLog.RetentionPeriod * -1),
                            _operator: "<")));
                Applications.SysLogsMaintenanceDate = DateTime.Now.Date;
            }
        }
    }
}
