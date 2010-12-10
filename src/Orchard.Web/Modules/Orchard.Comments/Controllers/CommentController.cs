﻿using System;
using System.Web.Mvc;
using Orchard.Comments.Models;
using Orchard.Comments.Services;
using Orchard.Comments.ViewModels;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Notify;
using Orchard.Utility.Extensions;

namespace Orchard.Comments.Controllers {
    public class CommentController : Controller {
        public IOrchardServices Services { get; set; }
        private readonly ICommentService _commentService;
        private readonly INotifier _notifier;

        public CommentController(IOrchardServices services, ICommentService commentService, INotifier notifier) {
            Services = services;
            _commentService = commentService;
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        [HttpPost]
        public ActionResult Create(string returnUrl) {
            if (!Services.Authorizer.Authorize(Permissions.AddComment, T("Couldn't add comment")))
                return !String.IsNullOrEmpty(returnUrl)
                    ? Redirect(returnUrl)
                    : Redirect("~/");
            
            var viewModel = new CommentsCreateViewModel();
            try {

                // UpdateModel(viewModel);

                if(!TryUpdateModel(viewModel)) {
                    if (Request.Form["Name"].IsNullOrEmptyTrimmed()) {
                        _notifier.Error(T("You must provide a Name in order to comment"));
                    }
                    return Redirect(returnUrl);
                }

                var context = new CreateCommentContext {
                                                           Author = viewModel.Name,
                                                           CommentText = viewModel.CommentText,
                                                           Email = viewModel.Email,
                                                           SiteName = viewModel.SiteName,
                                                           CommentedOn = viewModel.CommentedOn
                                                       };

                CommentPart commentPart = _commentService.CreateComment(context, Services.WorkContext.CurrentSite.As<CommentSettingsPart>().Record.ModerateComments);

                if (commentPart.Record.Status == CommentStatus.Pending)
                    Services.Notifier.Information(T("Your comment will appear after the site administrator approves it."));

                return !String.IsNullOrEmpty(returnUrl)
                    ? Redirect(returnUrl)
                    : Redirect("~/");
            }
            catch (Exception exception) {
                _notifier.Error(T("Creating Comment failed: " + exception.Message));
                // return View(viewModel);
                return Redirect(returnUrl);
            }
        }
    }
}