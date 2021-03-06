﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Models;
using System.Collections.Generic;
using System.Linq;
namespace Implem.Pleasanter.Libraries.HtmlParts
{
    public static class HtmlLinkCreations
    {
        public static HtmlBuilder LinkCreations(
            this HtmlBuilder hb,
            IContext context,
            SiteSettings ss,
            long linkId,
            BaseModel.MethodTypes methodType)
        {
            var links = Links(context: context, ss: ss);
            return
                methodType != BaseModel.MethodTypes.New &&
                links.Any()
                    ? hb.FieldSet(
                        css: " enclosed link-creations",
                        legendText: Displays.LinkCreations(context: context),
                        action: () => hb
                            .LinkCreations(
                                context: context,
                                ss: ss,
                                links: links,
                                linkId: linkId))
                    : hb;
        }

        private static List<Link> Links(IContext context, SiteSettings ss)
        {
            return new LinkCollection(
                context: context,
                column: Rds.LinksColumn()
                    .SourceId()
                    .DestinationId()
                    .SiteTitle(),
                where: Rds.LinksWhere()
                    .DestinationId(ss.SiteId)
                    .SiteId_In(ss.Sources?
                        .Where(currentSs => context.CanCreate(ss: currentSs))
                        .Select(currentSs => currentSs.SiteId)))
                            .Select(linkModel => GetLink(
                                linkModel: linkModel,
                                ss: ss.Sources.FirstOrDefault(o =>
                                    o.SiteId == linkModel.SourceId)))
                            .Where(o => o != null)
                            .ToList();
        }

        private static Link GetLink(LinkModel linkModel, SiteSettings ss)
        {
            var link = ss?.Links
                .Where(o => o.NoAddButton != true)
                .FirstOrDefault(o => o.SiteId == linkModel.DestinationId);
            if (link != null)
            {
                link.SiteTitle = linkModel.SiteTitle;
                link.SourceId = linkModel.SourceId;
            }
            return link;
        }

        private static HtmlBuilder LinkCreations(
            this HtmlBuilder hb,
            IContext context,
            SiteSettings ss,
            List<Link> links,
            long linkId)
        {
            return hb.Div(action: () => links.ForEach(link => hb
                .LinkCreationButton(
                    context: context,
                    ss: ss,
                    linkId: linkId,
                    sourceId: link.SourceId,
                    text: link.SiteTitle)));
        }

        private static HtmlBuilder LinkCreationButton(
            this HtmlBuilder hb,
            IContext context,
            SiteSettings ss,
            long linkId,
            long sourceId,
            string text)
        {
            return hb.Button(
                attributes: new HtmlAttributes()
                    .Class("button button-icon confirm-reload")
                    .OnClick("$p.new($(this));")
                    .Title(SiteInfo.TenantCaches.Get(context.TenantId)?
                        .SiteMenu
                        .Breadcrumb(context: context, siteId: sourceId)
                        .Select(o => o.Title).Join(" > "))
                    .DataId(linkId.ToString())
                    .DataIcon("ui-icon-plus")
                    .Add("data-from-site-id", ss.SiteId.ToString())
                    .Add("data-to-site-id", sourceId.ToString()),
                action: () => hb
                    .Text(text: text));
        }
    }
}