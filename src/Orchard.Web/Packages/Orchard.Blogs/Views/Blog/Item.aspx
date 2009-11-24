<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogViewModel>" %>
<%@ Import Namespace="Orchard.Core.Common.Models"%>
<%@ Import Namespace="Orchard.Models"%>
<%@ Import Namespace="Orchard.Blogs.Extensions"%>
<%@ Import Namespace="Orchard.Blogs.ViewModels"%>
<%@ Import Namespace="Orchard.Blogs.Models"%>
<%@ Import Namespace="Orchard.Mvc.Html"%>
<%@ Import Namespace="Orchard.Mvc.ViewModels"%>
<% Html.Include("Header"); %>
    <div class="yui-g">
        <h2>Blog</h2>
        <div><%=Html.Encode(Model.Blog.Name) %></div><%
        //TODO: (erikpo) Move this into a helper
        if (Model.Posts.Count() > 0) { %>
        <ul><%
            foreach (BlogPost post in Model.Posts) { %>
            <li><a href="<%=Url.BlogPost(post.Blog.Slug, post.As<RoutableAspect>().Slug) %>"><%=Html.Encode(post.As<RoutableAspect>().Title) %></a></li><%
            } %>
        </ul><%
        } %>
    </div>
<% Html.Include("Footer"); %>