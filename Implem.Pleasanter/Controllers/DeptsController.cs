﻿using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Models;
using System.Web;
using System.Web.Mvc;
namespace Implem.Pleasanter.Controllers
{
    public class DeptsController
    {
        public string Index(IContext context)
        {
            if (!context.Ajax)
            {
                var log = new SysLogModel(context: context);
                var html = DeptUtilities.Index(
                    context: context,
                    ss: SiteSettingsUtilities.DeptsSiteSettings(context: context));
                log.Finish(context: context, responseSize: html.Length);
                return html;
            }
            else
            {
                var log = new SysLogModel(context: context);
                var json = DeptUtilities.IndexJson(
                    context: context,
                    ss: SiteSettingsUtilities.DeptsSiteSettings(context: context));
                log.Finish(context: context, responseSize: json.Length);
                return json;
            }
        }

        public string New(IContext context, long id = 0)
        {
            var log = new SysLogModel(context: context);
            var html = DeptUtilities.EditorNew(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context));
            log.Finish(context: context, responseSize: html.Length);
            return html;
        }

        public string Edit(IContext context, int id)
        {
            if (!context.Ajax)
            {
                var log = new SysLogModel(context: context);
                var html = DeptUtilities.Editor(
                    context: context,
                    ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                    deptId: id,
                    clearSessions: true);
                log.Finish(context: context, responseSize: html.Length);
                return html;
            }
            else
            {
                var log = new SysLogModel(context: context);
                var json = DeptUtilities.EditorJson(
                    context: context,
                    ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                    deptId: id);
                log.Finish(context: context, responseSize: json.Length);
                return json;
            }
        }

        public string GridRows(IContext context)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.GridRows(context: context);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string Create(IContext context)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.Create(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context));
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string Update(IContext context, int id)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.Update(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                deptId: id);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string Delete(IContext context, int id)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.Delete(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                deptId: id);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string DeleteComment(IContext context, int id)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.Update(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                deptId: id);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string Histories(IContext context, int id)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.Histories(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                deptId: id);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }

        public string History(IContext context, int id)
        {
            var log = new SysLogModel(context: context);
            var json = DeptUtilities.History(
                context: context,
                ss: SiteSettingsUtilities.DeptsSiteSettings(context: context),
                deptId: id);
            log.Finish(context: context, responseSize: json.Length);
            return json;
        }
    }
}
