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
    public class OrderModel : BaseModel
    {
        public long ReferenceId = 0;
        public string ReferenceType = string.Empty;
        public int OwnerId = 0;
        public List<long> Data = new List<long>();
        [NonSerialized] public long SavedReferenceId = 0;
        [NonSerialized] public string SavedReferenceType = string.Empty;
        [NonSerialized] public int SavedOwnerId = 0;
        [NonSerialized] public string SavedData = "[]";

        public bool ReferenceId_Updated(IContext context, Column column = null)
        {
            return ReferenceId != SavedReferenceId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToLong() != ReferenceId);
        }

        public bool ReferenceType_Updated(IContext context, Column column = null)
        {
            return ReferenceType != SavedReferenceType && ReferenceType != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != ReferenceType);
        }

        public bool OwnerId_Updated(IContext context, Column column = null)
        {
            return OwnerId != SavedOwnerId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != OwnerId);
        }

        public bool Data_Updated(IContext context, Column column = null)
        {
            return Data.ToJson() != SavedData && Data.ToJson() != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != Data.ToJson());
        }

        public OrderModel(IContext context, DataRow dataRow, string tableAlias = null)
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

        public OrderModel Get(
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
                statements: Rds.SelectOrders(
                    tableType: tableType,
                    column: column ?? Rds.OrdersDefaultColumns(),
                    join: join ??  Rds.OrdersJoinDefault(),
                    where: where ?? Rds.OrdersWhereDefault(this),
                    orderBy: orderBy,
                    param: param,
                    distinct: distinct,
                    top: top)));
            return this;
        }

        public void SetByModel(OrderModel orderModel)
        {
            ReferenceId = orderModel.ReferenceId;
            ReferenceType = orderModel.ReferenceType;
            OwnerId = orderModel.OwnerId;
            Data = orderModel.Data;
            Comments = orderModel.Comments;
            Creator = orderModel.Creator;
            Updator = orderModel.Updator;
            CreatedTime = orderModel.CreatedTime;
            UpdatedTime = orderModel.UpdatedTime;
            VerUp = orderModel.VerUp;
            Comments = orderModel.Comments;
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
                        case "ReferenceType":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                ReferenceType = dataRow[column.ColumnName].ToString();
                                SavedReferenceType = ReferenceType;
                            }
                            break;
                        case "OwnerId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                OwnerId = dataRow[column.ColumnName].ToInt();
                                SavedOwnerId = OwnerId;
                            }
                            break;
                        case "Ver":
                            Ver = dataRow[column.ColumnName].ToInt();
                            SavedVer = Ver;
                            break;
                        case "Data":
                            Data = dataRow[column.ColumnName].ToString().Deserialize<List<long>>() ?? new List<long>();
                            SavedData = Data.ToJson();
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
                ReferenceType_Updated(context: context) ||
                OwnerId_Updated(context: context) ||
                Ver_Updated(context: context) ||
                Data_Updated(context: context) ||
                Comments_Updated(context: context) ||
                Creator_Updated(context: context) ||
                Updator_Updated(context: context);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public OrderModel()
        {
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public OrderModel(IContext context, SiteSettings ss, long referenceId, string referenceType)
        {
            ReferenceId = referenceId;
            ReferenceType = referenceType;
            OwnerId = referenceId == 0
                ? context.UserId
                : 0;
            Get(context: context);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public string UpdateOrCreate(
            IContext context,
            SqlWhereCollection where = null,
            SqlParamCollection param = null,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            Rds.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: Rds.UpdateOrInsertOrders(
                    where: where ?? Rds.OrdersWhereDefault(
                        orderModel: this),
                    param: param ?? Rds.OrdersParamDefault(
                        context: context,
                        orderModel: this,
                        setDefault: true),
                    tableType: tableType));
            return new ResponseCollection().ToJson();
        }
    }
}
