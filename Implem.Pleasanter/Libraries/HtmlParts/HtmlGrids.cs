﻿using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Settings;
using System.Collections.Generic;
namespace Implem.Pleasanter.Libraries.HtmlParts
{
    public static class HtmlGrids
    {
        public static HtmlBuilder GridHeader(
            this HtmlBuilder hb,
            IContext context,
            IEnumerable<Column> columns,
            View view = null,
            bool sort = true,
            bool checkAll = false,
            bool checkRow = true,
            string action = "GridRows")
        {
            return hb.Tr(
                css: "ui-widget-header",
                action: () =>
                {
                    if (checkRow)
                    {
                        hb.Th(action: () => hb
                            .CheckBox(
                                controlId: "GridCheckAll",
                                _checked: checkAll));
                    }
                    columns.ForEach(column =>
                    {
                        if (sort)
                        {
                            hb.Th(css: "sortable", action: () => hb
                                .Div(
                                    attributes: new HtmlAttributes()
                                        .DataId("ViewSorters__" + column.ColumnName)
                                        .Add("data-order-type", OrderBy(
                                            view, column.ColumnName))
                                        .DataAction(action)
                                        .DataMethod("post"),
                                    action: () => hb
                                        .Span(action: () => hb
                                            .Text(text: Displays.Get(
                                                context: context,
                                                id: column.GridLabelText)))
                                        .SortIcon(
                                            view: view,
                                            key: column.ColumnName)));
                        }
                        else
                        {
                            hb.Th(action: () => hb
                                .Text(text: Displays.Get(
                                    context: context,
                                    id: column.GridLabelText)));
                        }
                    });
                });
        }

        private static string OrderBy(View view, string key)
        {
            switch (view?.ColumnSorter(key))
            {
                case SqlOrderBy.Types.asc: return "desc";
                case SqlOrderBy.Types.desc: return string.Empty;
                default: return "asc";
            }
        }

        public static HtmlBuilder SortIcon(this HtmlBuilder hb, View view, string key)
        {
            switch (view?.ColumnSorter(key))
            {
                case SqlOrderBy.Types.asc: return hb.Icon(iconCss: "ui-icon-triangle-1-n");
                case SqlOrderBy.Types.desc: return hb.Icon(iconCss: "ui-icon-triangle-1-s");
                default: return hb;
            }
        }
    }
}