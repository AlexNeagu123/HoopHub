﻿using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class CommentContentMustBeValid(string content) : IBusinessRule
    {
        public bool IsBroken() => string.IsNullOrWhiteSpace(content) || content.Length < Config.ContentMinLength || content.Length > Config.ContentMaxLength;
        public string Message => ValidationErrors.InvalidCommentContent;
    }
}
