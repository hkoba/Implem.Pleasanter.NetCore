﻿using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Implem.Pleasanter.Libraries.Settings
{
    public static class SiteSettingsUtilities
    {
        public const decimal Version = 1.014M;

        public static SiteSettings Get(
            IContext context,
            long siteId,
            long referenceId = 0,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            return Get(
                context: context,
                siteModel: new SiteModel(context: context, siteId: siteId),
                referenceId: referenceId != 0
                    ? referenceId
                    : siteId,
                setSiteIntegration: setSiteIntegration,
                setAllChoices: setAllChoices,
                tableType: tableType);
        }

        public static SiteSettings Get(IContext context, DataRow dataRow)
        {
            return dataRow != null
                ? dataRow["SiteSettings"]
                    .ToString()
                    .Deserialize<SiteSettings>() ??
                        Get(
                            context: context,
                            referenceType: dataRow.String("ReferenceType"),
                            siteId: dataRow.Long("SiteId"))
                : null;
        }

        public static SiteSettings Get(this List<SiteSettings> ssList, long siteId)
        {
            return ssList.FirstOrDefault(o => o.SiteId == siteId);
        }

        public static View Get(this List<View> views, int? id)
        {
            return views?.FirstOrDefault(o => o.Id == id);
        }

        public static SiteSettings Get(IContext context, string referenceType, long siteId)
        {
            switch (referenceType)
            {
                case "Sites": return SitesSiteSettings(context: context, siteId: siteId);
                case "Issues": return IssuesSiteSettings(context: context, siteId: siteId);
                case "Results": return ResultsSiteSettings(context: context, siteId: siteId);
                case "Wikis": return WikisSiteSettings(context: context, siteId: siteId);
                default: return new SiteSettings() { SiteId = siteId };
            }
        }

        public static SiteSettings Get(
            IContext context,
            SiteModel siteModel,
            long referenceId,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            switch (siteModel.ReferenceType)
            {
                case "Sites": return SitesSiteSettings(
                    context: context,
                    siteModel: siteModel,
                    referenceId: referenceId,
                    setSiteIntegration: setSiteIntegration,
                    setAllChoices: setAllChoices,
                    tableType: tableType);
                case "Issues": return IssuesSiteSettings(
                    context: context,
                    siteModel: siteModel,
                    referenceId: referenceId,
                    setSiteIntegration: setSiteIntegration,
                    setAllChoices: setAllChoices,
                    tableType: tableType);
                case "Results": return ResultsSiteSettings(
                    context: context,
                    siteModel: siteModel,
                    referenceId: referenceId,
                    setSiteIntegration: setSiteIntegration,
                    setAllChoices: setAllChoices,
                    tableType: tableType);
                case "Wikis": return WikisSiteSettings(
                    context: context,
                    siteModel: siteModel,
                    referenceId: referenceId,
                    setSiteIntegration: setSiteIntegration,
                    setAllChoices: setAllChoices,
                    tableType: tableType);
                default: return new SiteSettings() { SiteId = siteModel.SiteId };
            }
        }

        public static SiteSettings GetByReference(
            IContext context, string reference, long referenceId)
        {
            switch (reference.ToLower())
            {
                case "tenants": return TenantsSiteSettings(context: context);
                case "demos": return DemosSiteSettings(context: context);
                case "sessions": return SessionsSiteSettings(context: context);
                case "syslogs": return SysLogsSiteSettings(context: context);
                case "statuses": return StatusesSiteSettings(context: context);
                case "reminderschedules": return ReminderSchedulesSiteSettings(context: context);
                case "depts": return DeptsSiteSettings(context: context);
                case "groups": return GroupsSiteSettings(context: context);
                case "groupmembers": return GroupMembersSiteSettings(context: context);
                case "users": return UsersSiteSettings(context: context);
                case "loginkeys": return LoginKeysSiteSettings(context: context);
                case "mailaddresses": return MailAddressesSiteSettings(context: context);
                case "outgoingmails": return OutgoingMailsSiteSettings(context: context);
                case "searchindexes": return SearchIndexesSiteSettings(context: context);
                case "orders": return OrdersSiteSettings(context: context);
                case "exportsettings": return ExportSettingsSiteSettings(context: context);
                case "links": return LinksSiteSettings(context: context);
                case "binaries": return BinariesSiteSettings(context: context);
                case "items": return Get(
                    context: context,
                    siteModel: new ItemModel(
                        context: context,
                        referenceId: referenceId)
                            .GetSite(context: context),
                    referenceId: referenceId);
                default: return null;
            }
        }

        public static SiteSettings TenantsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Tenants"
            };
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, withLink: false);
            ss.PermissionType = Permissions.Admins(context: context);
            return ss;
        }

        public static SiteSettings DemosSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Demos"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings SessionsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Sessions"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings SysLogsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "SysLogs"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings StatusesSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Statuses"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings ReminderSchedulesSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "ReminderSchedules"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings DeptsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Depts"
            };
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, withLink: false);
            ss.PermissionType = Permissions.Admins(context: context);
            return ss;
        }

        public static SiteSettings GroupsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Groups"
            };
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, withLink: false);
            ss.PermissionType = Permissions.Admins(context: context);
            return ss;
        }

        public static SiteSettings GroupMembersSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "GroupMembers"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings UsersSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Users"
            };
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, withLink: false);
            ss.PermissionType = Permissions.Admins(context: context);
            return ss;
        }

        public static SiteSettings LoginKeysSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "LoginKeys"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings MailAddressesSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "MailAddresses"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings OutgoingMailsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "OutgoingMails"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings SearchIndexesSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "SearchIndexes"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings ItemsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Items"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings OrdersSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Orders"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings ExportSettingsSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "ExportSettings"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings LinksSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Links"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings BinariesSiteSettings(IContext context)
        {
            var ss = new SiteSettings()
            {
                ReferenceType = "Binaries"
            };
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings SitesSiteSettings(
            this SiteModel siteModel,
            IContext context,
            long referenceId,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            var ss = siteModel.SiteSettings ?? new SiteSettings();
            ss.TableType = tableType;
            ss.SiteId = siteModel.SiteId;
            ss.ReferenceId = referenceId;
            ss.Title = siteModel.Title.Value;
            ss.ReferenceType = "Sites";
            ss.ParentId = siteModel.ParentId;
            ss.InheritPermission = siteModel.InheritPermission;
            ss.Publish = siteModel.Publish;
            ss.AccessStatus = siteModel.AccessStatus;
            ss.Init(context: context);
            ss.SetLinkedSiteSettings(context: context);
            ss.SetPermissions(context: context, referenceId: referenceId);
            ss.SetJoinedSsHash(context: context);
            if (setSiteIntegration) ss.SetSiteIntegration(context: context);
            return ss;
        }

        public static SiteSettings SitesSiteSettings(
            IContext context, long siteId, bool setAllChoices = false)
        {
            var ss = new SiteSettings();
            ss.ReferenceType = "Sites";
            ss.SiteId = siteId;
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings IssuesSiteSettings(
            this SiteModel siteModel,
            IContext context,
            long referenceId,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            var ss = siteModel.SiteSettings ?? new SiteSettings();
            ss.TableType = tableType;
            ss.SiteId = siteModel.SiteId;
            ss.ReferenceId = referenceId;
            ss.Title = siteModel.Title.Value;
            ss.ReferenceType = "Issues";
            ss.ParentId = siteModel.ParentId;
            ss.InheritPermission = siteModel.InheritPermission;
            ss.Publish = siteModel.Publish;
            ss.AccessStatus = siteModel.AccessStatus;
            ss.Init(context: context);
            ss.SetLinkedSiteSettings(context: context);
            ss.SetPermissions(context: context, referenceId: referenceId);
            ss.SetJoinedSsHash(context: context);
            if (setSiteIntegration) ss.SetSiteIntegration(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings IssuesSiteSettings(
            IContext context, long siteId, bool setAllChoices = false)
        {
            var ss = new SiteSettings();
            ss.ReferenceType = "Issues";
            ss.SiteId = siteId;
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings ResultsSiteSettings(
            this SiteModel siteModel,
            IContext context,
            long referenceId,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            var ss = siteModel.SiteSettings ?? new SiteSettings();
            ss.TableType = tableType;
            ss.SiteId = siteModel.SiteId;
            ss.ReferenceId = referenceId;
            ss.Title = siteModel.Title.Value;
            ss.ReferenceType = "Results";
            ss.ParentId = siteModel.ParentId;
            ss.InheritPermission = siteModel.InheritPermission;
            ss.Publish = siteModel.Publish;
            ss.AccessStatus = siteModel.AccessStatus;
            ss.Init(context: context);
            ss.SetLinkedSiteSettings(context: context);
            ss.SetPermissions(context: context, referenceId: referenceId);
            ss.SetJoinedSsHash(context: context);
            if (setSiteIntegration) ss.SetSiteIntegration(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings ResultsSiteSettings(
            IContext context, long siteId, bool setAllChoices = false)
        {
            var ss = new SiteSettings();
            ss.ReferenceType = "Results";
            ss.SiteId = siteId;
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings WikisSiteSettings(
            this SiteModel siteModel,
            IContext context,
            long referenceId,
            bool setSiteIntegration = false,
            bool setAllChoices = false,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            var ss = siteModel.SiteSettings ?? new SiteSettings();
            ss.TableType = tableType;
            ss.SiteId = siteModel.SiteId;
            ss.ReferenceId = referenceId;
            ss.Title = siteModel.Title.Value;
            ss.ReferenceType = "Wikis";
            ss.ParentId = siteModel.ParentId;
            ss.InheritPermission = siteModel.InheritPermission;
            ss.Publish = siteModel.Publish;
            ss.AccessStatus = siteModel.AccessStatus;
            ss.Init(context: context);
            ss.SetLinkedSiteSettings(context: context);
            ss.SetPermissions(context: context, referenceId: referenceId);
            ss.SetJoinedSsHash(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings WikisSiteSettings(
            IContext context, long siteId, bool setAllChoices = false)
        {
            var ss = new SiteSettings();
            ss.ReferenceType = "Wikis";
            ss.SiteId = siteId;
            ss.Init(context: context);
            ss.SetChoiceHash(context: context, all: setAllChoices);
            return ss;
        }

        public static SiteSettings PermissionsSiteSettings(
            this SiteModel siteModel, IContext context)
        {
            var ss = new SiteSettings();
            ss.ReferenceType = "Permissions";
            ss.SiteId = siteModel.SiteId;
            ss.InheritPermission = siteModel.InheritPermission;
            ss.ParentId = siteModel.ParentId;
            ss.Title = siteModel.Title.Value;
            ss.AccessStatus = siteModel.AccessStatus;
            ss.Init(context: context);
            return ss;
        }

        public static SiteSettings GetByDataRow(IContext context, long siteId)
        {
            var dataRow = Rds.ExecuteTable(
                context: context,
                statements: Rds.SelectSites(
                    column: Rds.SitesColumn()
                        .SiteSettings()
                        .Title()
                        .InheritPermission(),
                    where: Rds.SitesWhere()
                        .TenantId(context.TenantId)
                        .SiteId(siteId)))
                            .AsEnumerable()
                            .FirstOrDefault();
            if (dataRow != null)
            {
                var ss = dataRow
                    .String("SiteSettings")
                    .Deserialize<SiteSettings>() ?? new SiteSettings();
                ss.SiteId = siteId;
                ss.Title = dataRow.String("Title");
                ss.InheritPermission = dataRow.Long("InheritPermission");
                return ss;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static SiteSettings ApiUsersSiteSettings(IContext context)
        {
            var ss = UsersSiteSettings(context);
            ss?.Columns?
                .Where(c => c.Name == "Disabled")?
                .ForEach(c => c.CheckFilterControlType = ColumnUtilities.CheckFilterControlTypes.OnAndOff);
            ss?.EditorColumns?.Clear();
            new[] { "Password" }.ForEach(c => ss.GridColumns.Remove(c));
            if (context.User?.TenantManager != true)
            {
                ss.GridColumns = new List<string>() { "UserId", "LoginId", "Name", "Disabled" };
            }
            return ss;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static SiteSettings ApiGroupsSiteSettings(IContext context)
        {
            var ss = GroupsSiteSettings(context);
            ss?.Columns?
                .Where(c => c.Name == "Disabled")?
                .ForEach(c => c.CheckFilterControlType = ColumnUtilities.CheckFilterControlTypes.OnAndOff);
            return ss;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static SiteSettings ApiDeptsSiteSettings(IContext context)
        {
            var ss = DeptsSiteSettings(context);
            ss?.Columns?
                .Where(c => c.Name == "Disabled")?
                .ForEach(c => c.CheckFilterControlType = ColumnUtilities.CheckFilterControlTypes.OnAndOff);
            return ss;
        }
    }
}
