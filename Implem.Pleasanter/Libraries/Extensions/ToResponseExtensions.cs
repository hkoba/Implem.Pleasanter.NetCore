﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using System;
using System.Collections.Generic;
namespace Implem.Pleasanter.Libraries.Extensions
{
    public static class ToResponseExtensions
    {
        public static string ToResponse(
            this Enum self, IContext context, SiteSettings ss, Column column)
        {
            return self.ToString();
        }

        public static string ToResponse(
            this bool self, IContext context, SiteSettings ss, Column column)
        {
            return self.ToString();
        }

        public static string ToResponse(
            this DateTime self, IContext context, SiteSettings ss, Column column)
        {
            return self.InRange()
                ? column.DisplayControl(
                    context: context,
                    value: self.ToLocal(context: context))
                : string.Empty;
        }

        public static string ToResponse(
            this int self, IContext context, SiteSettings ss, Column column)
        {
            return self.ToString();
        }

        public static string ToResponse(
            this long self, IContext context, SiteSettings ss, Column column)
        {
            return self.ToString();
        }

        public static string ToResponse(
            this decimal self, IContext context, SiteSettings ss, Column column)
        {
            return column.ControlType == "Spinner"
                ? column.Display(
                    context: context,
                    value: self,
                    format: false)
                : column.Display(
                    context: context,
                    ss: ss,
                    value: self,
                    format: column.Format == "C");
        }

        public static string ToResponse(
            this string self, IContext context, SiteSettings ss, Column column)
        {
            return column.EditorReadOnly != true || !column.HasChoices()
                ? self.ToString()
                : column.Choice(self).Text;
        }
    }
}