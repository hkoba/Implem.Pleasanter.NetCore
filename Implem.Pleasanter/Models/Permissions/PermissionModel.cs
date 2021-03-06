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
    [Serializable]
    public class PermissionModel : BaseModel
    {
        public long ReferenceId = 0;
        public int DeptId = 0;
        public int GroupId = 0;
        public int UserId = 0;
        public string DeptName = string.Empty;
        public string GroupName = string.Empty;
        public string Name = string.Empty;
        public Permissions.Types PermissionType = (Permissions.Types)31;
        [NonSerialized] public long SavedReferenceId = 0;
        [NonSerialized] public int SavedDeptId = 0;
        [NonSerialized] public int SavedGroupId = 0;
        [NonSerialized] public int SavedUserId = 0;
        [NonSerialized] public string SavedDeptName = string.Empty;
        [NonSerialized] public string SavedGroupName = string.Empty;
        [NonSerialized] public string SavedName = string.Empty;
        [NonSerialized] public long SavedPermissionType = 31;

        public bool ReferenceId_Updated(IContext context, Column column = null)
        {
            return ReferenceId != SavedReferenceId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToLong() != ReferenceId);
        }

        public bool DeptId_Updated(IContext context, Column column = null)
        {
            return DeptId != SavedDeptId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != DeptId);
        }

        public bool GroupId_Updated(IContext context, Column column = null)
        {
            return GroupId != SavedGroupId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != GroupId);
        }

        public bool UserId_Updated(IContext context, Column column = null)
        {
            return UserId != SavedUserId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != UserId);
        }

        public bool PermissionType_Updated(IContext context, Column column = null)
        {
            return PermissionType.ToLong() != SavedPermissionType &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToLong() != PermissionType.ToLong());
        }

        public PermissionModel(IContext context, DataRow dataRow, string tableAlias = null)
        {
            OnConstructing(context: context);
            Context = context;
            if (dataRow != null) Set(context, dataRow, tableAlias);
            OnConstructed(context: context);
        }

        private void OnConstructing(IContext context)
        {
        }

        private void OnConstructed(IContext context)
        {
        }

        public void ClearSessions(IContext context)
        {
        }

        public PermissionModel Get(
            IContext context,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlColumnCollection column = null,
            SqlJoinCollection join = null,
            SqlWhereCollection where = null,
            SqlOrderByCollection orderBy = null,
            SqlParamCollection param = null,
            bool distinct = false,
            int top = 0)
        {
            Set(context, Rds.ExecuteTable(
                context: context,
                statements: Rds.SelectPermissions(
                    tableType: tableType,
                    column: column ?? Rds.PermissionsDefaultColumns(),
                    join: join ??  Rds.PermissionsJoinDefault(),
                    where: where ?? Rds.PermissionsWhereDefault(this),
                    orderBy: orderBy,
                    param: param,
                    distinct: distinct,
                    top: top)));
            return this;
        }

        public void SetByModel(PermissionModel permissionModel)
        {
            ReferenceId = permissionModel.ReferenceId;
            DeptId = permissionModel.DeptId;
            GroupId = permissionModel.GroupId;
            UserId = permissionModel.UserId;
            DeptName = permissionModel.DeptName;
            GroupName = permissionModel.GroupName;
            Name = permissionModel.Name;
            PermissionType = permissionModel.PermissionType;
            Comments = permissionModel.Comments;
            Creator = permissionModel.Creator;
            Updator = permissionModel.Updator;
            CreatedTime = permissionModel.CreatedTime;
            UpdatedTime = permissionModel.UpdatedTime;
            VerUp = permissionModel.VerUp;
            Comments = permissionModel.Comments;
        }

        private void SetBySession(IContext context)
        {
        }

        private void Set(IContext context, DataTable dataTable)
        {
            switch (dataTable.Rows.Count)
            {
                case 1: Set(context, dataTable.Rows[0]); break;
                case 0: AccessStatus = Databases.AccessStatuses.NotFound; break;
                default: AccessStatus = Databases.AccessStatuses.Overlap; break;
            }
        }

        private void Set(IContext context, DataRow dataRow, string tableAlias = null)
        {
            AccessStatus = Databases.AccessStatuses.Selected;
            foreach(DataColumn dataColumn in dataRow.Table.Columns)
            {
                var column = new ColumnNameInfo(dataColumn.ColumnName);
                if (column.TableAlias == tableAlias)
                {
                    switch (column.Name)
                    {
                        case "ReferenceId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                ReferenceId = dataRow[column.ColumnName].ToLong();
                                SavedReferenceId = ReferenceId;
                            }
                            break;
                        case "DeptId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                DeptId = dataRow[column.ColumnName].ToInt();
                                SavedDeptId = DeptId;
                            }
                            break;
                        case "GroupId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                GroupId = dataRow[column.ColumnName].ToInt();
                                SavedGroupId = GroupId;
                            }
                            break;
                        case "UserId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                UserId = dataRow[column.ColumnName].ToInt();
                                SavedUserId = UserId;
                            }
                            break;
                        case "Ver":
                            Ver = dataRow[column.ColumnName].ToInt();
                            SavedVer = Ver;
                            break;
                        case "DeptName":
                            DeptName = dataRow[column.ColumnName].ToString();
                            SavedDeptName = DeptName;
                            break;
                        case "GroupName":
                            GroupName = dataRow[column.ColumnName].ToString();
                            SavedGroupName = GroupName;
                            break;
                        case "Name":
                            Name = dataRow[column.ColumnName].ToString();
                            SavedName = Name;
                            break;
                        case "PermissionType":
                            PermissionType = (Permissions.Types)dataRow[column.ColumnName].ToLong();
                            SavedPermissionType = PermissionType.ToLong();
                            break;
                        case "Comments":
                            Comments = dataRow[column.ColumnName].ToString().Deserialize<Comments>() ?? new Comments();
                            SavedComments = Comments.ToJson();
                            break;
                        case "Creator":
                            Creator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedCreator = Creator.Id;
                            break;
                        case "Updator":
                            Updator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedUpdator = Updator.Id;
                            break;
                        case "CreatedTime":
                            CreatedTime = new Time(context, dataRow, column.ColumnName);
                            SavedCreatedTime = CreatedTime.Value;
                            break;
                        case "UpdatedTime":
                            UpdatedTime = new Time(context, dataRow, column.ColumnName); Timestamp = dataRow.Field<DateTime>(column.ColumnName).ToString("yyyy/M/d H:m:s.fff");
                            SavedUpdatedTime = UpdatedTime.Value;
                            break;
                        case "IsHistory": VerType = dataRow[column.ColumnName].ToBool() ? Versions.VerTypes.History : Versions.VerTypes.Latest; break;
                    }
                }
            }
        }

        public bool Updated(IContext context)
        {
            return
                ReferenceId_Updated(context: context) ||
                DeptId_Updated(context: context) ||
                GroupId_Updated(context: context) ||
                UserId_Updated(context: context) ||
                Ver_Updated(context: context) ||
                PermissionType_Updated(context: context) ||
                Comments_Updated(context: context) ||
                Creator_Updated(context: context) ||
                Updator_Updated(context: context);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public PermissionModel(
            IContext context,
            long referenceId,
            int deptId,
            int groupId,
            int userId,
            Permissions.Types permissionType)
        {
            ReferenceId = referenceId;
            if (deptId != 0)
            {
                DeptId = deptId;
                DeptName = SiteInfo.Dept(
                    tenantId: context.TenantId,
                    deptId: DeptId)?
                        .Name;
            }
            if (groupId != 0)
            {
                GroupId = groupId;
                GroupName = new GroupModel(
                    context: context,
                    ss: SiteSettingsUtilities.GroupsSiteSettings(context: context),
                    groupId: GroupId)?
                        .GroupName;
            }
            if (userId != 0)
            {
                UserId = userId;
                var user = SiteInfo.User(
                    context: context,
                    userId: UserId);
                Name = user?.Name;
            }
            PermissionType = permissionType;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        /// <param name="dataRow"></param>
        public PermissionModel(
            IContext context,
            long referenceId,
            Permissions.Types permissionType,
            DataRow dataRow)
        {
            OnConstructing(context: context);
            ReferenceId = referenceId;
            PermissionType = permissionType;
            Set(context: context, dataRow: dataRow);
            OnConstructed(context: context);
        }
    }
}
